$root = (Get-Location).Path
$sim = Join-Path $root 'Examen_Modulo_Practico_2\Simulacion'
if(-not (Test-Path $sim)){
	Write-Host "Simulacion folder not found: $sim"
	exit 1
}
$compFile = Join-Path $root 'Examen_Modulo_Practico_2\ComponentRepository.cs'
$names = Select-String -Path $compFile -Pattern 'C\("[^\"]*", "([^\"]+)"' -AllMatches | ForEach-Object { $_.Matches } | ForEach-Object { $_.Groups[1].Value } | Sort-Object -Unique

function Normalize([string]$s){
	if([string]::IsNullOrWhiteSpace($s)){ return '' }
	$n = $s.ToLowerInvariant()
	$n = $n.Normalize([Text.NormalizationForm]::FormD)
	$sb = New-Object System.Text.StringBuilder
	foreach($ch in $n.ToCharArray()){
		$cat = [Globalization.CharUnicodeInfo]::GetUnicodeCategory($ch)
		if($cat -eq [Globalization.UnicodeCategory]::NonSpacingMark){ continue }
		if([char]::IsLetterOrDigit($ch)){ $sb.Append($ch) }
	}
	return $sb.ToString()
}

function Sanitize([string]$s){
	if($null -eq $s){ return '' }
	$invalid=[IO.Path]::GetInvalidFileNameChars()
	$out = $s
	foreach($c in $invalid){ $out = $out -replace [regex]::Escape($c), '-' }
	$out = $out -replace '/','-'
	$out = $out -replace '\\','-'
	$out = $out -replace '[:*?"<>|]','-'
	$out = $out -replace '\s+',' '
	$out = $out.Trim()
	return $out
}

$exts = @('*.png','*.jpg','*.jpeg','*.webp')
$files = Get-ChildItem -Path $sim -File -Include $exts
$fileMap = @{ }
foreach($f in $files){ \$fileMap\[$(Normalize($f.BaseName))\] = $f }

$renamed = 0; $skipped = 0; $missing = @()
foreach($comp in $names){
	$norm = Normalize($comp)
	if($fileMap.ContainsKey($norm)){
		$f = $fileMap[$norm]
		$newName = Sanitize($comp) + $f.Extension
		$newPath = Join-Path $sim $newName
		if($f.Name -ieq $newName){ $skipped++; continue }
		if(Test-Path $newPath){ Write-Host "Target already exists: $newName -> skipping"; $skipped++; continue }
		Write-Host "Renaming $($f.Name) -> $newName"
		Move-Item -LiteralPath $f.FullName -Destination $newPath
		$renamed++
	} else {
		$missing += $comp
	}
}
Write-Host "Done. Renamed: $renamed, Skipped: $skipped, Missing: $($missing.Count)"
if($missing.Count -gt 0){ Write-Host 'Missing sample:'; $missing[0..[Math]::Min(9,$missing.Count-1)] | ForEach-Object { Write-Host $_ } }

