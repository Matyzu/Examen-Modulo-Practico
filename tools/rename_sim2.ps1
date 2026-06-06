# Rename simulation images to match component names from ComponentRepository
$root = (Get-Location).Path
$sim = Join-Path $root 'Examen_Modulo_Practico_2\Simulacion'
$compFile = Join-Path $root 'Examen_Modulo_Practico_2\ComponentRepository.cs'

if (-not (Test-Path $sim)) { Write-Error "Simulacion folder not found: $sim"; exit 1 }
if (-not (Test-Path $compFile)) { Write-Error "ComponentRepository.cs not found: $compFile"; exit 1 }

# Read component names
$names = Select-String -Path $compFile -Pattern 'C\("[^\"]*", "([^\"]+)"' -AllMatches |
	ForEach-Object { $_.Matches } |
	ForEach-Object { $_.Groups[1].Value } | Sort-Object -Unique

function Normalize([string]$s) {
	if ([string]::IsNullOrWhiteSpace($s)) { return "" }
	$n = $s.ToLowerInvariant()
	$n = $n.Normalize([Text.NormalizationForm]::FormD)
	$sb = New-Object System.Text.StringBuilder
	foreach ($ch in $n.ToCharArray()) {
		$cat = [Globalization.CharUnicodeInfo]::GetUnicodeCategory($ch)
		if ($cat -eq [Globalization.UnicodeCategory]::NonSpacingMark) { continue }
		if ([char]::IsLetterOrDigit($ch)) { [void]$sb.Append($ch) }
	}
	return $sb.ToString()
}

function Sanitize([string]$s) {
	if ($null -eq $s) { return "" }
	$invalid = [IO.Path]::GetInvalidFileNameChars()
	$out = $s
	foreach ($c in $invalid) { $out = $out -replace [regex]::Escape($c), '-' }
	$out = $out -replace '/', '-'
	$out = $out -replace '\\', '-'
	$out = $out -replace '[:*?"<>|]', '-'
	$out = $out -replace '\s+', ' '
	$out = $out.Trim()
	return $out
}

function Levenshtein([string]$s,[string]$t) {
	if ($s -eq $null) { $s = "" }
	if ($t -eq $null) { $t = "" }
	$n = $s.Length
	$m = $t.Length
	if ($n -eq 0) { return $m }
	if ($m -eq 0) { return $n }
	$d = New-Object 'int[,]' ($n + 1), ($m + 1)
	for ($i = 0; $i -le $n; $i++) { $d[$i,0] = $i }
	for ($j = 0; $j -le $m; $j++) { $d[0,$j] = $j }
	for ($i = 1; $i -le $n; $i++) {
		for ($j = 1; $j -le $m; $j++) {
			if ($s[$i - 1] -eq $t[$j - 1]) { $cost = 0 } else { $cost = 1 }
			$d[$i,$j] = [Math]::Min([Math]::Min($d[$i - 1,$j] + 1, $d[$i,$j - 1] + 1), $d[$i - 1,$j - 1] + $cost)
		}
	}
	return $d[$n,$m]
}

$exts = @('*.png','*.jpg','*.jpeg','*.webp')
$files = Get-ChildItem -Path $sim -File -Include $exts

# build file info list
$fileInfos = @()
foreach ($f in $files) {
	$fileInfos += [pscustomobject]@{
		Path = $f.FullName
		Name = $f.Name
		Base = $f.BaseName
		Ext = $f.Extension
		Norm = Normalize($f.BaseName)
	}
}

$renamed = 0
$skipped = 0
$notMatched = @()

foreach ($comp in $names) {
	$targetNorm = Normalize($comp)
	if ([string]::IsNullOrWhiteSpace($targetNorm)) { $notMatched += $comp; continue }

	# find best match by levenshtein
	$best = $null
	$bestScore = [int]::MaxValue
	foreach ($fi in $fileInfos) {
		$score = Levenshtein($fi.Norm, $targetNorm)
		if ($score -lt $bestScore) { $bestScore = $score; $best = $fi }
	}

	if ($best -ne $null) {
		# threshold: allow small differences
		$threshold = [Math]::Max(1, [int]([Math]::Ceiling($targetNorm.Length / 4)))
		if ($bestScore -le $threshold) {
			$newName = (Sanitize($comp) + $best.Ext)
			$newPath = Join-Path $sim $newName
			if ((Test-Path $newPath) -and ((Get-Item $newPath).FullName -ieq $best.Path)) {
				$skipped++
			} elseif (Test-Path $newPath) {
				Write-Host "Conflict: target exists for $comp -> $newName, skipping"
				$skipped++
			} else {
				Write-Host "Renaming '$($best.Name)' -> '$newName' (score $bestScore)"
				Move-Item -LiteralPath $best.Path -Destination $newPath
				$renamed++
				# update fileInfos: remove old and add new entry
				$fileInfos = $fileInfos | Where-Object { $_.Path -ne $best.Path }
				$fileInfos += [pscustomobject]@{ Path = (Join-Path $sim $newName); Name = $newName; Base = [IO.Path]::GetFileNameWithoutExtension($newName); Ext = $best.Ext; Norm = Normalize([IO.Path]::GetFileNameWithoutExtension($newName)) }
			}
			continue
		}
	}

	$notMatched += $comp
}

Write-Host "Done. Renamed: $renamed, Skipped: $skipped, NotMatched: $($notMatched.Count)"
if ($notMatched.Count -gt 0) {
	Write-Host "Not matched sample (up to 20):"
	$notMatched[0..[Math]::Min(19,$notMatched.Count-1)] | ForEach-Object { Write-Host " - $_" }
}
