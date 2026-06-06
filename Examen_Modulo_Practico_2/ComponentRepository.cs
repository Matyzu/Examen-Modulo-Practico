using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using System.Linq;

namespace Examen_Modulo_Practico_2
{
    public static class ComponentRepository
    {
        public static List<ComponentInfo> All { get; } =
        new List<ComponentInfo>
        {
            C("Pasivos", "Resistencia", "Limitacion y division", "Se opone al paso de corriente y permite controlar voltajes y corrientes.", "Limitar corriente de LEDs, crear divisores de voltaje y proteger entradas.", "Se mide en ohmios. No tiene polaridad.", "Tip: revisa el codigo de colores o mide con multimetro."),
            C("Pasivos", "Capacitor ceramico", "Filtrado rapido", "Almacena pequenas cargas y responde bien a cambios rapidos de senal.", "Eliminar ruido cerca de microcontroladores e integrados.", "Valores comunes: nF y pF. No tiene polaridad.", "Tip: colocalo cerca del pin de alimentacion del integrado."),
            C("Pasivos", "Capacitor electrolitico", "Reserva de energia", "Almacena mas carga que uno ceramico y ayuda a estabilizar la alimentacion.", "Filtrar fuentes, suavizar picos y sostener voltaje por momentos cortos.", "Tiene polaridad. Su capacitancia suele estar en uF.", "Tip: invertirlo puede danarlo o hacerlo explotar."),
            C("Pasivos", "Inductor", "Campo magnetico", "Almacena energia en forma de campo magnetico y se opone a cambios de corriente.", "Filtros, fuentes conmutadas y circuitos de radiofrecuencia.", "Se mide en henrios. Tiene resistencia interna.", "Tip: revisa su corriente maxima antes de usarlo en potencia."),
            C("Pasivos", "Potenciometro", "Resistencia variable", "Permite variar manualmente una resistencia mediante una perilla o eje.", "Control de volumen, brillo, sensibilidad o ajustes analogicos.", "Tiene tres terminales: dos extremos y un cursor central.", "Tip: conectado como divisor entrega un voltaje variable."),
            C("Pasivos", "Trimmer", "Ajuste fino", "Es un potenciometro pequeno pensado para calibracion interna.", "Ajustar precision en sensores, fuentes o circuitos de medicion.", "Se regula con destornillador y no suele moverse a diario.", "Tip: ideal para calibraciones que deben quedar fijas."),
            C("Pasivos", "Termistor NTC", "Temperatura", "Resistencia que baja su valor cuando la temperatura aumenta.", "Medicion de temperatura y proteccion contra picos de corriente.", "NTC significa coeficiente de temperatura negativo.", "Tip: se usa con divisor de voltaje para leerlo en un ADC."),
            C("Pasivos", "LDR", "Luz", "Resistencia que cambia segun la cantidad de luz recibida.", "Detectores de dia/noche, alarmas simples y luces automaticas.", "Su resistencia baja cuando recibe mas luz.", "Tip: responde lento, pero es muy facil de usar."),
            C("Pasivos", "Fusible", "Proteccion", "Se abre cuando la corriente supera un valor seguro.", "Proteger equipos, fuentes y circuitos contra cortocircuitos.", "Se especifica por corriente y voltaje maximo.", "Tip: nunca reemplaces un fusible por un cable."),
            C("Pasivos", "Cristal de cuarzo", "Tiempo y frecuencia", "Genera una frecuencia estable para sincronizar circuitos digitales.", "Reloj de microcontroladores, RTC y modulos de comunicacion.", "Valores comunes: 16 MHz, 8 MHz y 32.768 kHz.", "Tip: suele requerir capacitores pequenos a tierra."),

            C("Activos", "Diodo rectificador", "Conduccion en un sentido", "Permite el paso de corriente en una sola direccion.", "Rectificar corriente alterna y proteger contra polaridad inversa.", "Tiene anodo y catodo. Ejemplo popular: 1N4007.", "Tip: la banda del cuerpo marca el catodo."),
            C("Activos", "LED", "Diodo emisor de luz", "Emite luz cuando circula corriente en sentido correcto.", "Indicadores visuales, senalizacion y pruebas de estado.", "Tiene polaridad y necesita resistencia limitadora.", "Tip: la pata larga suele ser el anodo."),
            C("Activos", "Diodo Zener", "Regulacion", "Mantiene un voltaje casi constante cuando trabaja en inversa.", "Referencias de voltaje y proteccion contra sobretension.", "Se elige por voltaje Zener y potencia.", "Tip: debe usarse con resistencia para limitar corriente."),
            C("Activos", "Transistor BJT NPN", "Conmutacion y amplificacion", "Controla corriente entre colector y emisor usando la base.", "Encender cargas pequenas, amplificar senales y manejar reles.", "Terminales: base, colector y emisor.", "Tip: necesita resistencia en la base."),
            C("Activos", "Transistor BJT PNP", "Conmutacion positiva", "Similar al NPN, pero conduce cuando la base esta mas baja que el emisor.", "Interruptores del lado positivo y etapas complementarias.", "La corriente principal va de emisor a colector.", "Tip: revisa bien la polaridad antes de conectarlo."),
            C("Activos", "MOSFET canal N", "Control de potencia", "Controla cargas con muy poca corriente en la compuerta.", "Motores, tiras LED, bombas y fuentes conmutadas.", "Terminales: gate, drain y source.", "Tip: para microcontroladores conviene uno logic level."),
            C("Activos", "Optoacoplador", "Aislamiento", "Transmite una senal usando luz interna sin conexion electrica directa.", "Aislar microcontroladores de etapas de potencia o AC.", "Incluye LED interno y fototransistor.", "Tip: mejora seguridad cuando hay voltajes altos."),
            C("Activos", "Regulador 7805", "Regulacion lineal", "Entrega 5 V estables desde una entrada mayor.", "Alimentar circuitos logicos, sensores y microcontroladores antiguos.", "Pines tipicos: entrada, tierra y salida.", "Tip: puede calentarse; usa disipador si consume mucho."),
            C("Activos", "Amplificador operacional", "Procesamiento analogico", "Amplifica diferencias de voltaje y permite crear filtros o comparadores.", "Audio, sensores, filtros, comparadores y acondicionamiento de senal.", "Ejemplos comunes: LM358, LM741, TL081.", "Tip: revisa si funciona con fuente simple o dual."),
            C("Activos", "Circuito integrado 555", "Temporizador", "Integrado clasico para generar retardos, pulsos y oscilaciones.", "Parpadeo de LEDs, alarmas, timers y generadores de frecuencia.", "Modos populares: monoestable y astable.", "Tip: se combina con resistencias y capacitores para definir tiempos."),
            C("Activos", "Microcontrolador ATmega328P", "Control programable", "Microcontrolador de 8 bits usado en placas Arduino Uno y Nano.", "Proyectos educativos, control de sensores, automatizacion y prototipos embebidos.", "Trabaja comunmente a 5 V y 16 MHz en Arduino.", "Tip: necesita bootloader o programador ISP para cargar firmware."),
            C("Activos", "ESP32", "Microcontrolador IoT", "Microcontrolador de 32 bits con WiFi y Bluetooth integrados.", "Internet de las cosas, servidores web, sensores remotos y control por celular.", "Logica de 3.3 V; muchas placas aceptan 5 V por USB/VIN.", "Tip: no conectes 5 V directo a pines GPIO."),
            C("Activos", "Raspberry Pi Pico RP2040", "Microcontrolador dual-core", "Placa con microcontrolador RP2040, dos nucleos ARM Cortex-M0+ y muchas GPIO.", "Educacion, control digital, PIO, sensores, robots y automatizacion.", "Logica de 3.3 V y alimentacion por USB o VSYS.", "Tip: excelente para tareas rapidas sin sistema operativo."),
            C("Activos", "Driver L293D", "Puente H", "Circuito integrado para controlar motores DC y motores paso a paso pequenos.", "Robots, carros, ruedas, motores bidireccionales y practicas de control.", "Puede manejar dos motores DC o un motor paso a paso bipolar.", "Tip: para motores grandes usa drivers mas modernos como TB6612FNG."),
            C("Activos", "Driver ULN2003", "Arreglo Darlington", "Integrado con transistores Darlington para manejar cargas desde senales logicas.", "Motores paso a paso 28BYJ-48, reles, LEDs y cargas inductivas pequenas.", "Incluye diodos de proteccion para cargas inductivas.", "Tip: solo hunde corriente; la carga va al positivo."),
            C("Activos", "Registro 74HC595", "Expansion de salidas", "Registro de desplazamiento de 8 bits que aumenta salidas usando pocos pines.", "Matrices LED, displays, indicadores y proyectos con muchos LEDs.", "Usa datos seriales y entrega 8 salidas paralelas.", "Tip: se pueden encadenar varios para mas salidas."),
            C("Activos", "Contador CD4017", "Contador decada", "Integrado contador que activa diez salidas secuenciales con pulsos de reloj.", "Secuenciadores LED, efectos de luces, contadores simples y divisores.", "Trabaja con familias CMOS y requiere senal de clock estable.", "Tip: ideal para aprender secuencias sin programar."),
            C("Activos", "Multiplexor CD4051", "Selector analogico", "Permite seleccionar una de ocho senales analogicas o digitales con tres lineas.", "Expandir entradas analogicas, seleccionar sensores y conmutar senales.", "Tiene 8 canales, 3 bits de seleccion y pin enable.", "Tip: respeta limites de voltaje entre VEE, VSS y VDD."),
            C("Activos", "Amplificador LM386", "Audio", "Amplificador de baja potencia para altavoces pequenos.", "Radios, alarmas, intercomunicadores y salidas de audio simples.", "Puede trabajar con bateria y pocos componentes externos.", "Tip: usa capacitores de acoplo para evitar ruido y DC en el parlante."),
            C("Activos", "Driver MAX7219", "Control de matrices LED", "Integrado que controla displays de 7 segmentos o matrices LED con interfaz serial.", "Relojes, contadores, indicadores y matrices 8x8.", "Maneja multiplexado y corriente de LEDs internamente.", "Tip: reduce muchisimo el cableado frente a controlar LEDs directo."),

            C("Entradas", "Pulsador", "Entrada digital", "Cierra o abre un contacto solo mientras se presiona.", "Botones de inicio, reset, seleccion y control manual.", "Puede requerir resistencia pull-up o pull-down.", "Tip: aplica antirrebote por software o hardware."),
            C("Entradas", "Interruptor", "Control manual", "Mantiene un estado encendido o apagado hasta cambiarlo manualmente.", "Encendido general, seleccion de modo y seguridad.", "Tipos comunes: SPST, SPDT y DPDT.", "Tip: elige uno que soporte la corriente de la carga."),
            C("Entradas", "Sensor PIR", "Movimiento", "Detecta movimiento por cambios de radiacion infrarroja.", "Alarmas, luces automaticas y sistemas de presencia.", "Normalmente entrega una senal digital.", "Tip: necesita unos segundos para estabilizarse al encender."),
            C("Entradas", "Sensor ultrasonico HC-SR04", "Distancia", "Mide distancia enviando sonido y calculando el eco recibido.", "Robots, medidores de nivel y deteccion de obstaculos.", "Usa pines Trigger y Echo.", "Tip: funciona mejor con superficies planas."),
            C("Entradas", "Sensor de temperatura LM35", "Temperatura analogica", "Entrega un voltaje proporcional a la temperatura.", "Termometros, control de ventiladores y monitoreo ambiental.", "Salida aproximada: 10 mV por grado Celsius.", "Tip: protege el cableado si esta lejos del microcontrolador."),
            C("Entradas", "Sensor DHT11", "Temperatura y humedad", "Modulo digital simple para medir temperatura y humedad.", "Estaciones climaticas escolares y monitoreo de ambientes.", "Usa una linea de datos digital.", "Tip: no es muy rapido ni muy preciso, pero es facil de usar."),
            C("Entradas", "Sensor de gas MQ-2", "Gas y humo", "Detecta gases combustibles y humo mediante una resistencia sensible.", "Alarmas de gas, humo y practicas de seguridad.", "Necesita calentamiento para estabilizar lectura.", "Tip: consume mas corriente que muchos sensores pequenos."),
            C("Entradas", "Microfono electret", "Sonido", "Convierte vibraciones de sonido en una pequena senal electrica.", "Detectores de aplausos, audio basico y medicion de ruido.", "Suele necesitar polarizacion y amplificacion.", "Tip: un modulo con amplificador es mas facil para principiantes."),
            C("Entradas", "Teclado matricial 4x4", "Entrada numerica", "Agrupa botones en filas y columnas para leer muchos pulsadores con pocos pines.", "Claves, menus, calculadoras y controles de acceso.", "Tiene 8 pines para 16 teclas.", "Tip: se escanean filas y columnas desde el programa."),
            C("Entradas", "Joystick analogico", "Control de direccion", "Entrega dos ejes analogicos y un pulsador central.", "Robots, menus, videojuegos y control de servos.", "Salidas comunes: VRx, VRy y SW.", "Tip: sus ejes se leen con entradas analogicas."),

            C("Salidas", "LED indicador", "Luz simple", "Muestra estados de un circuito mediante luz.", "Indicadores de encendido, error, carga o actividad.", "Necesita resistencia limitadora.", "Tip: usa colores distintos para estados distintos."),
            C("Salidas", "Display de 7 segmentos", "Visual numerico", "Muestra numeros usando segmentos LED.", "Contadores, relojes, termometros y medidores.", "Puede ser anodo comun o catodo comun.", "Tip: usa multiplexado si necesitas varios digitos."),
            C("Salidas", "Pantalla LCD 16x2", "Texto", "Muestra dos lineas de texto con 16 caracteres por linea.", "Menus, mediciones, mensajes y proyectos con Arduino.", "Puede usar interfaz paralela o modulo I2C.", "Tip: con I2C reduces mucho el cableado."),
            C("Salidas", "Pantalla OLED I2C", "Grafica pequena", "Pantalla compacta con buen contraste y bajo consumo.", "Menus modernos, iconos, sensores y datos en tiempo real.", "Resolucion comun: 128x64 pixeles.", "Tip: revisa su direccion I2C antes de programar."),
            C("Salidas", "Buzzer activo", "Sonido", "Produce sonido al recibir voltaje sin generar una frecuencia externa.", "Alarmas, avisos, temporizadores y confirmaciones.", "Solo necesita alimentacion y control on/off.", "Tip: es mas facil que un buzzer pasivo."),
            C("Salidas", "Buzzer pasivo", "Tono controlado", "Necesita una senal de frecuencia para producir sonidos.", "Melodias, tonos personalizados y alarmas variables.", "La frecuencia define el tono.", "Tip: se controla bien con PWM."),
            C("Salidas", "Rele", "Conmutacion aislada", "Interruptor accionado electricamente para controlar cargas externas.", "Encender luces, bombas, ventiladores y cargas AC o DC.", "Tiene bobina y contactos COM, NO y NC.", "Tip: usa diodo flyback si manejas la bobina directamente."),
            C("Salidas", "Motor DC", "Movimiento continuo", "Convierte energia electrica en giro.", "Robots, ventiladores, ruedas y mecanismos simples.", "Velocidad depende del voltaje y la carga.", "Tip: usa transistor o driver; no lo conectes directo al pin."),
            C("Salidas", "Servomotor", "Posicion angular", "Gira a una posicion especifica segun una senal de control.", "Brazos roboticos, puertas, direccion de robots y mecanismos.", "Usa alimentacion, tierra y senal PWM.", "Tip: alimentalo con fuente externa si consume mucho."),
            C("Salidas", "Tira LED RGB", "Iluminacion", "Conjunto de LEDs que permite mezclar colores.", "Decoracion, senalizacion, efectos visuales y proyectos creativos.", "Puede ser analogica o direccionable como WS2812B.", "Tip: calcula la corriente total antes de elegir la fuente."),

            C("Alimentacion", "Bateria 9 V", "Fuente portatil", "Fuente compacta para proyectos de bajo consumo.", "Prototipos pequenos, multimetros y circuitos de prueba.", "No entrega mucha corriente por mucho tiempo.", "Tip: no es ideal para motores o cargas grandes."),
            C("Alimentacion", "Portapilas AA", "Energia modular", "Permite alimentar circuitos con pilas comunes AA.", "Robots simples, controles y proyectos escolares.", "Cada pila alcalina entrega cerca de 1.5 V.", "Tip: cuatro pilas AA dan aproximadamente 6 V."),
            C("Alimentacion", "Celda Li-ion 18650", "Recargable", "Bateria recargable de alta capacidad usada en equipos modernos.", "Power banks, linternas, robots y proyectos portatiles.", "Voltaje nominal: 3.7 V; cargada: 4.2 V.", "Tip: requiere cargador y proteccion adecuados."),
            C("Alimentacion", "Modulo cargador TP4056", "Carga Li-ion", "Carga celdas de litio de una celda con control de corriente.", "Proyectos recargables con baterias 18650 o LiPo.", "Entrada comun: 5 V por USB.", "Tip: usa version con proteccion si la bateria no la incluye."),
            C("Alimentacion", "Regulador buck LM2596", "Convertidor step-down", "Reduce un voltaje mayor a uno menor con buena eficiencia.", "Bajar 12 V a 5 V o 7.4 V a 5 V.", "Es ajustable con potenciometro.", "Tip: ajusta la salida antes de conectar tu circuito."),
            C("Alimentacion", "Convertidor boost MT3608", "Convertidor step-up", "Eleva un voltaje bajo a uno mayor.", "Subir 3.7 V de bateria a 5 V para modulos.", "La corriente de entrada aumenta al subir voltaje.", "Tip: no excedas su corriente maxima real."),
            C("Alimentacion", "Fuente de protoboard MB102", "Prototipado", "Modulo que entrega voltajes utiles directamente a una protoboard.", "Practicas con 3.3 V y 5 V en laboratorio.", "Suele tener salidas seleccionables por jumper.", "Tip: revisa la posicion de los jumpers antes de alimentar."),
            C("Alimentacion", "Adaptador AC/DC", "Fuente externa", "Convierte energia de la pared a voltaje DC seguro para equipos.", "Alimentar routers, tiras LED, placas y modulos.", "Se especifica por voltaje, corriente y polaridad.", "Tip: verifica centro positivo o centro negativo."),
            C("Alimentacion", "Diodo de proteccion", "Antipolaridad", "Evita dano si se conecta una fuente al reves.", "Entrada de alimentacion en proyectos portatiles.", "Puede ponerse en serie o en paralelo con fusible.", "Tip: un Schottky pierde menos voltaje que uno comun."),
            C("Alimentacion", "Capacitor de filtro", "Estabilidad", "Reduce variaciones de voltaje y ayuda durante picos de consumo.", "Entrada y salida de reguladores, motores y modulos inestables.", "Combina electroliticos grandes con ceramicos pequenos.", "Tip: respeta polaridad y voltaje maximo del capacitor.")
        };

        public static List<ComponentInfo> BySection(string section)
        {
            return All.Where(component => component.Section == section).ToList();
        }

        public static List<ComponentInfo> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<ComponentInfo>();
            }

            string normalizedQuery = query.Trim();

            return All
                .Where(component =>
                    Contains(component.Name, normalizedQuery) ||
                    Contains(component.Category, normalizedQuery) ||
                    Contains(component.Section, normalizedQuery) ||
                    Contains(component.Description, normalizedQuery) ||
                    Contains(component.CommonUse, normalizedQuery) ||
                    Contains(component.KeyData, normalizedQuery))
                .Take(12)
                .ToList();
        }

        private static ComponentInfo C(string section, string name, string category, string description, string commonUse, string keyData, string tip)
        {
            ComponentInfo component = new ComponentInfo
            {
                Section = section,
                Name = name,
                Category = category,
                Description = description,
                CommonUse = commonUse,
                KeyData = keyData,
                Tip = tip,
                ImagePath = $"Assets/Components/{ToAssetName(name)}.png",
                SimulationImagePath = ResolveSimulationPath(name),
                ExtraDatasheets = BuildDatasheetLinks(name)
            };

            ApplyTechnicalProfile(component);
            return component;
        }

        private static void ApplyTechnicalProfile(ComponentInfo component)
        {
            component.TechnicalDetails = component.Name switch
            {
                "Resistencia" => "Controla corriente por oposicion electrica. En circuitos digitales se usa como pull-up, pull-down y limitadora.",
                "Capacitor ceramico" => "Muy usado para desacoplo porque responde rapido ante ruido de alta frecuencia.",
                "Capacitor electrolitico" => "Aporta reserva de energia y suaviza variaciones lentas de voltaje en fuentes.",
                "Diodo rectificador" => "Conduce en directa y bloquea en inversa hasta su limite maximo.",
                "LED" or "LED indicador" => "Convierte corriente en luz. Su color depende del material semiconductor.",
                "Transistor BJT NPN" or "Transistor BJT PNP" => "Trabaja como interruptor o amplificador segun la corriente aplicada a la base.",
                "MOSFET canal N" => "Se controla por voltaje en la compuerta y es eficiente para cargas de potencia.",
                "Regulador 7805" => "Regulador lineal clasico. La diferencia entre entrada y salida se disipa como calor.",
                "Circuito integrado 555" => "Temporizador analogico muy usado para osciladores, retardos y generadores de pulsos.",
                "Sensor ultrasonico HC-SR04" => "Calcula distancia midiendo el tiempo entre el pulso enviado y el eco recibido.",
                "Sensor DHT11" => "Entrega temperatura y humedad mediante protocolo digital de una sola linea.",
                "Rele" => "Usa una bobina para mover contactos mecanicos y aislar control de potencia.",
                "Servomotor" => "Integra motor, engranajes y control interno para posicionarse por pulsos PWM.",
                "Regulador buck LM2596" => "Convertidor conmutado que reduce voltaje con mayor eficiencia que un regulador lineal.",
                "Convertidor boost MT3608" => "Convertidor conmutado que eleva voltaje, aumentando la corriente tomada desde la entrada.",
                _ => "Ficha tecnica resumida para reconocer conexion, valores tipicos, precauciones y aplicacion practica del componente."
            };

            component.Pins = component.Name switch
            {
                "Resistencia" or "Fusible" or "LDR" or "Termistor NTC" or "Inductor" => "2 terminales sin polaridad.",
                "Capacitor ceramico" => "2 terminales sin polaridad.",
                "Capacitor electrolitico" or "LED" or "LED indicador" or "Diodo rectificador" or "Diodo Zener" or "Diodo de proteccion" => "2 terminales con polaridad: anodo/positivo y catodo/negativo.",
                "Potenciometro" or "Trimmer" => "3 terminales: extremo 1, cursor central y extremo 2.",
                "Transistor BJT NPN" or "Transistor BJT PNP" => "3 pines: base, colector y emisor.",
                "MOSFET canal N" => "3 pines: gate, drain y source.",
                "Regulador 7805" => "3 pines tipicos: entrada, tierra y salida.",
                "Amplificador operacional" => "Pines de alimentacion, entradas inversora/no inversora y salida.",
                "Circuito integrado 555" => "8 pines: GND, Trigger, Output, Reset, Control, Threshold, Discharge y VCC.",
                "Sensor ultrasonico HC-SR04" => "4 pines: VCC, Trigger, Echo y GND.",
                "Sensor DHT11" => "3 o 4 pines segun modulo: VCC, Data y GND.",
                "Joystick analogico" => "5 pines comunes: VCC, GND, VRx, VRy y SW.",
                "Teclado matricial 4x4" => "8 pines: 4 filas y 4 columnas.",
                "Servomotor" => "3 cables: VCC, GND y senal PWM.",
                "Motor DC" => "2 terminales para alimentacion del motor.",
                "Rele" => "Bobina y contactos: COM, NO y NC.",
                _ => "Revisar hoja de datos o serigrafia del modulo antes de conectar."
            };

            component.NominalValues = component.Name switch
            {
                "Resistencia" => "Valores comunes: 220 ohm, 1 kohm, 10 kohm. Potencia tipica: 1/4 W.",
                "Capacitor ceramico" => "Valores comunes: 100 nF, 10 nF, 1 nF. Voltajes: 16 V a 50 V.",
                "Capacitor electrolitico" => "Valores comunes: 10 uF a 1000 uF. Voltajes: 16 V, 25 V, 50 V.",
                "Inductor" => "Valores comunes: uH a mH. Importa corriente maxima y resistencia interna.",
                "LED" or "LED indicador" => "Caida tipica: rojo 1.8-2.2 V, verde/azul/blanco 2.8-3.3 V. Corriente comun: 10-20 mA.",
                "Diodo rectificador" => "Caida aproximada: 0.7 V. Ejemplo 1N4007: hasta 1 A.",
                "Diodo Zener" => "Voltajes populares: 3.3 V, 5.1 V, 9.1 V, 12 V.",
                "Transistor BJT NPN" or "Transistor BJT PNP" => "VBE aproximado: 0.7 V. Corriente depende del modelo.",
                "MOSFET canal N" => "Revisar VGS(th), RDS(on), corriente maxima y si es logic level.",
                "Regulador 7805" => "Salida fija: 5 V. Entrada comun: 7 V a 12 V. Corriente depende del encapsulado y disipacion.",
                "Sensor ultrasonico HC-SR04" => "Alimentacion tipica: 5 V. Rango aproximado: 2 cm a 400 cm.",
                "Sensor DHT11" => "Alimentacion: 3.3 V a 5 V. Rango: 0-50 C y 20-90% HR aproximado.",
                "Rele" => "Bobinas comunes: 5 V, 12 V o 24 V. Contactos dependen del modelo.",
                "Servomotor" => "Alimentacion comun: 4.8 V a 6 V. Senal PWM cercana a 50 Hz.",
                "Regulador buck LM2596" => "Entrada comun hasta 40 V segun modulo. Salida ajustable.",
                "Convertidor boost MT3608" => "Entrada aproximada: 2 V a 24 V. Salida ajustable hasta valores mayores segun carga.",
                _ => "Valores nominales variables segun fabricante. Confirmar con datasheet para diseno final."
            };

            component.Identification = component.Name switch
            {
                "Resistencia" => "Se identifica por bandas de colores o por codigo impreso en SMD.",
                "Capacitor electrolitico" => "La franja lateral suele marcar el negativo; el valor aparece impreso en el cuerpo.",
                "Capacitor ceramico" => "Puede tener codigo de tres digitos, por ejemplo 104 equivale a 100 nF.",
                "LED" or "LED indicador" => "Pata larga: anodo. Lado plano del encapsulado: catodo.",
                "Diodo rectificador" or "Diodo Zener" or "Diodo de proteccion" => "La banda pintada en el cuerpo marca el catodo.",
                "Potenciometro" or "Trimmer" => "El pin central suele ser el cursor variable.",
                "Transistor BJT NPN" or "Transistor BJT PNP" or "MOSFET canal N" => "El orden de pines cambia por modelo; revisar serigrafia o datasheet.",
                "Rele" => "Los contactos suelen estar dibujados en el encapsulado o modulo.",
                "Bateria 9 V" or "Celda Li-ion 18650" or "Portapilas AA" => "Identificar positivo y negativo antes de conectar.",
                _ => "Reconocer por nombre del modulo, serigrafia, forma fisica y hoja tecnica."
            };

            component.SafetyNotes = component.Name switch
            {
                "Capacitor electrolitico" => "No invertir polaridad ni superar su voltaje maximo.",
                "Fusible" => "Debe reemplazarse por otro del mismo tipo y corriente.",
                "Diodo rectificador" or "Diodo Zener" or "LED" or "LED indicador" => "Usar resistencia o limitacion de corriente cuando corresponda.",
                "MOSFET canal N" or "Transistor BJT NPN" or "Transistor BJT PNP" => "No exceder corriente, voltaje ni disipacion maxima.",
                "Rele" or "Motor DC" or "Servomotor" => "Usar fuente adecuada y proteccion contra picos inductivos.",
                "Celda Li-ion 18650" or "Modulo cargador TP4056" => "Evitar cortocircuitos, sobrecarga y descarga excesiva.",
                "Adaptador AC/DC" => "Verificar voltaje, corriente y polaridad del conector.",
                _ => "Conectar primero en protoboard o simulacion y medir antes de usar con cargas reales."
            };

            component.ElectricalCharacteristics = component.Name switch
            {
                "Microcontrolador ATmega328P" => "Flash 32 KB, SRAM 2 KB, EEPROM 1 KB, ADC de 10 bits, timers de 8/16 bits, PWM e interrupciones externas.",
                "ESP32" => "CPU dual-core hasta 240 MHz, WiFi 2.4 GHz, Bluetooth, ADC, DAC, PWM, timers, SPI, I2C, UART e I2S.",
                "Raspberry Pi Pico RP2040" => "Dual ARM Cortex-M0+ hasta 133 MHz, 264 KB SRAM, ADC de 12 bits, USB 1.1, PWM y bloques PIO.",
                "Driver L293D" => "Puente H doble con entradas TTL, enable por canal, diodos internos y alimentacion separada para logica/motor.",
                "Driver ULN2003" => "Siete salidas Darlington open collector con diodos clamp para cargas inductivas.",
                "Registro 74HC595" => "Registro serial-paralelo de 8 bits con latch, salida serial Q7S, OE y reset maestro.",
                "Contador CD4017" => "Contador Johnson CMOS de 10 salidas decodificadas con clock, reset, enable y carry out.",
                "Multiplexor CD4051" => "Selector analogico/digital de 8 canales con tres lineas de direccion y pin inhibit.",
                "Amplificador LM386" => "Amplificador de baja potencia; ganancia por defecto 20 y configurable hasta 200 con capacitor externo.",
                "Driver MAX7219" => "Controlador multiplexado para 8 digitos o matriz 8x8, brillo programable, shutdown y test display.",
                _ => $"Magnitud principal: {component.KeyData} Revisar tolerancia, potencia, corriente maxima, temperatura y margen de seguridad."
            };

            component.PackageFormats = component.Name switch
            {
                "Microcontrolador ATmega328P" => "DIP-28 para protoboard, TQFP-32 y QFN/MLF para placas compactas.",
                "ESP32" => "Modulos ESP-WROOM/ESP-WROVER y placas DevKit con USB y regulador integrado.",
                "Raspberry Pi Pico RP2040" => "Placa Raspberry Pi Pico/Pico W; chip RP2040 en QFN-56.",
                "Circuito integrado 555" or "Amplificador LM386" => "DIP-8 para protoboard y SOIC-8 en montaje superficial.",
                "Driver L293D" or "Driver ULN2003" or "Registro 74HC595" or "Contador CD4017" or "Multiplexor CD4051" => "DIP-16 para practicas y versiones SMD segun fabricante.",
                "Driver MAX7219" => "DIP-24, SOIC-24 y modulos listos con matriz LED o displays.",
                "Regulador 7805" => "TO-220, TO-92, SOT-223 y encapsulados SMD.",
                _ => "Puede encontrarse como componente discreto, SMD o modulo de prototipado."
            };

            component.TypicalCircuit = component.Name switch
            {
                "Microcontrolador ATmega328P" => "VCC/AVCC a 5 V, GND comun, RESET con pull-up, cristal de 16 MHz con capacitores y desacoplo de 100 nF.",
                "ESP32" => "Alimentacion estable de 3.3 V, EN con pull-up, GPIO solo a 3.3 V, sensores por I2C/SPI/UART y capacitor cerca del modulo.",
                "Raspberry Pi Pico RP2040" => "USB o VSYS, GND comun, sensores a 3.3 V y cargas externas controladas con transistor, rele o driver.",
                "Driver L293D" => "Entradas desde microcontrolador, salidas al motor, VCC1 logica, VCC2 motor, enable activo y GND comun.",
                "Driver ULN2003" => "GPIO a entradas, carga entre positivo y salida, GND comun y COM al positivo cuando hay bobinas.",
                "Registro 74HC595" => "SER, SRCLK y RCLK desde microcontrolador; Q0-Q7 hacia LEDs con resistencias; OE a GND y MR a VCC.",
                "Contador CD4017" => "Clock desde 555 o microcontrolador, reset controlado y salidas Q0-Q9 a LEDs con resistencias.",
                "Multiplexor CD4051" => "Comun al ADC o senal principal, X0-X7 a sensores, A/B/C a GPIO y alimentacion acorde a la senal.",
                "Amplificador LM386" => "Entrada con capacitor de acoplo, potenciometro de volumen, salida con capacitor hacia parlante y filtrado en VCC.",
                "Driver MAX7219" => "DIN, CLK y LOAD al microcontrolador; matriz o display a DIG/SEG; resistencia ISET para corriente.",
                _ => "Conexion tipica: alimentacion correcta, GND comun, componente en serie/paralelo segun su funcion y proteccion adecuada."
            };

            component.CommunicationProtocol = component.Name switch
            {
                "Microcontrolador ATmega328P" => "GPIO, ADC, PWM, UART, SPI e I2C/TWI.",
                "ESP32" => "WiFi, Bluetooth, GPIO, ADC, DAC, PWM, UART, SPI, I2C, I2S y touch.",
                "Raspberry Pi Pico RP2040" => "USB, GPIO, PWM, ADC, UART, SPI, I2C y PIO programable.",
                "Sensor DHT11" => "Protocolo digital propietario de una sola linea.",
                "Sensor ultrasonico HC-SR04" => "Control por pulsos Trigger/Echo; no usa bus serial.",
                "Registro 74HC595" => "Serial sincronico compatible con SPI simple.",
                "Driver MAX7219" => "Interfaz serial tipo SPI de 3 lineas: DIN, CLK y LOAD.",
                "Pantalla LCD 16x2" => "Paralelo de 4/8 bits o I2C si usa adaptador.",
                "Pantalla OLED I2C" => "I2C, normalmente con controlador SSD1306.",
                _ => "No aplica protocolo digital directo; se controla con conexion electrica, senal analogica, digital o PWM."
            };

            component.DesignRecommendations = component.Name switch
            {
                "Microcontrolador ATmega328P" or "ESP32" or "Raspberry Pi Pico RP2040" => "Agregar capacitores de desacoplo, cuidar niveles logicos, documentar pines usados y separar potencia de logica.",
                "Driver L293D" or "Driver ULN2003" => "Usar fuente externa para motores/reles, compartir GND con el controlador y dimensionar cables para la corriente.",
                "Registro 74HC595" or "Driver MAX7219" => "Cables cortos para reloj/datos, capacitor de 100 nF cerca del integrado y limitar corriente de LEDs.",
                "Amplificador LM386" => "Mantener la entrada de audio lejos de la salida, filtrar alimentacion y usar masa comun ordenada.",
                "Multiplexor CD4051" => "No aplicar senales fuera del rango de alimentacion; ideal para sensores de baja corriente.",
                _ => "Seleccionar por calculo, comprobar polaridad, medir antes de energizar y dejar margen de seguridad."
            };

            component.FailureSymptoms = component.Name switch
            {
                "Microcontrolador ATmega328P" or "ESP32" or "Raspberry Pi Pico RP2040" => "No programa, reinicios, calentamiento, consumo alto, pines sin respuesta o comunicacion inestable.",
                "Driver L293D" or "Driver ULN2003" => "Motor sin fuerza, integrado caliente, salidas fijas, ruido electrico o reinicios del microcontrolador.",
                "Registro 74HC595" or "Contador CD4017" or "Driver MAX7219" => "LEDs invertidos, brillo bajo, datos desplazados, salidas aleatorias o falta de clock/latch.",
                "Amplificador LM386" => "Ruido, distorsion, volumen bajo, oscilacion, calentamiento o ausencia de audio.",
                _ => "Lecturas inestables, calentamiento, olor a quemado, valores fuera de rango o funcionamiento intermitente."
            };

            ApplyAdvancedPinOverrides(component);
            ApplyDatasheetNarrative(component);
        }

        private static void ApplyDatasheetNarrative(ComponentInfo component)
        {
            component.DatasheetOverview =
                $"Este registro resume al {component.Name} como componente de la seccion {component.Section}. Su funcion principal es {component.Category.ToLowerInvariant()}: {component.Description} En una hoja de datos real se revisan primero los valores maximos, el encapsulado, la forma de conexion y las condiciones recomendadas de trabajo antes de montarlo en una practica. Para esta ficha educativa se combinan datos de identificacion, uso comun, pines y precauciones para que el estudiante pueda reconocerlo, conectarlo y compararlo con otros componentes similares.";

            component.OperatingNotes =
                $"Condiciones recomendadas: trabajar dentro de los valores nominales indicados, usar una fuente estable y verificar continuidad, polaridad o sentido de conexion antes de energizar. {component.NominalValues} {component.ElectricalCharacteristics} Si el componente forma parte de un modulo, tambien se debe revisar la serigrafia de la placa, los jumpers, el regulador integrado y la corriente que puede entregar o consumir durante el funcionamiento normal.";

            component.SelectionGuide =
                $"Criterios de seleccion: elegir el {component.Name} segun la aplicacion, el margen electrico, el encapsulado disponible y la facilidad de montaje. Para reemplazos conviene comparar pinout, voltaje, corriente, potencia, tolerancia, temperatura de trabajo y disponibilidad comercial. {component.PackageFormats} {component.DesignRecommendations} Si dos modelos parecen equivalentes, el datasheet original debe confirmar que soportan las mismas condiciones de prueba.";

            component.LaboratoryChecklist =
                $"Lista de verificacion en laboratorio: identificar visualmente el componente, ubicar sus terminales, medir valores basicos con multimetro cuando sea posible, armar primero una conexion simple y comprobar el comportamiento antes de integrarlo al circuito completo. {component.Identification} {component.SafetyNotes} Senales de alerta: {component.FailureSymptoms} Registrar mediciones ayuda a comparar la teoria con el comportamiento real del montaje.";

            component.CommonUseDetails =
                $"{component.CommonUse} En una practica real se coloca dentro de una etapa concreta del circuito: entrada, procesamiento, potencia, visualizacion o alimentacion. Antes de usarlo conviene definir que variable controla, que senal recibe, que carga alimenta o que proteccion aporta. Tambien es util dibujar su conexion en el esquema y anotar que pasa si se retira, se invierte o se reemplaza por otro valor.";

            component.NominalValuesDetails =
                $"{component.NominalValues} Para seleccionar el valor correcto se comparan las condiciones normales con los limites maximos permitidos. En laboratorio se recomienda dejar margen de seguridad, medir el voltaje real de la fuente y considerar tolerancia, temperatura, potencia disipada y corriente de trabajo. Si el circuito va a funcionar varias horas, el valor nominal no debe quedar justo al limite.";

            component.PinsDetails =
                $"{component.Pins} La numeracion o posicion de terminales puede cambiar entre fabricantes, encapsulados y modulos. Por eso se debe confirmar el pinout con la serigrafia, la muesca, el punto de referencia o el datasheet original. En protoboard se recomienda marcar VCC, GND, entradas y salidas antes de energizar para evitar cruces accidentales.";

            component.PackageFormatsDetails =
                $"{component.PackageFormats} La presentacion afecta el montaje, la disipacion de calor, la facilidad de reemplazo y el espacio ocupado en la placa. Los encapsulados grandes son comodos para practicas, mientras que los SMD permiten placas compactas pero exigen mas cuidado al soldar. En modulos listos tambien deben revisarse reguladores, jumpers, conectores y pines rotulados.";

            component.CommunicationProtocolDetails =
                $"{component.CommunicationProtocol} Cuando existe una senal de control, hay que revisar niveles logicos, frecuencia, temporizacion, direccion de datos y compatibilidad con el microcontrolador. Si no usa protocolo digital, se analiza como elemento analogico o de potencia: caida de voltaje, corriente, resistencia, polaridad, PWM, conexion serie/paralelo o aislamiento.";

            component.SafetyNotesDetails =
                $"{component.SafetyNotes} Como regla de prueba, primero se energiza con baja corriente o una fuente limitada, se toca con cuidado solo si no hay alto voltaje, y se verifica que no exista calentamiento anormal. La polaridad, el sentido de conexion, el aislamiento y la corriente maxima son puntos criticos. Si aparece olor, ruido, reinicio del circuito o temperatura excesiva, se debe apagar y revisar conexiones.";

            component.DesignRecommendationsDetails =
                $"{component.DesignRecommendations} En el diseno final conviene agregar etiquetas claras en el esquema, puntos de medicion y margen para reemplazos. Tambien se recomienda separar senales sensibles de cargas de potencia, mantener cables cortos cuando haya conmutacion o reloj, y documentar el valor elegido con una nota de calculo sencilla. Esto hace mas facil diagnosticar fallas despues.";

            component.FailureSymptomsDetails =
                $"{component.FailureSymptoms} Para diagnosticar, se compara el comportamiento esperado con mediciones reales: voltaje de entrada, salida, continuidad, corriente consumida y temperatura. Muchas fallas vienen de pines invertidos, falta de tierra comun, fuente insuficiente, soldadura fria o valor incorrecto. Probar el componente por separado ayuda a saber si el problema esta en el componente o en el circuito.";

            component.TechnicalDetailsExtended =
                $"{component.TechnicalDetails} En terminos de datasheet, esta seccion describe la funcion interna, la variable electrica mas importante y la forma en que el componente modifica el comportamiento del circuito. Para analizarlo se revisa si trabaja como elemento de control, carga, proteccion, sensor, actuador o fuente de energia. Tambien se compara su comportamiento ideal con el real: tolerancia, caida de voltaje, disipacion, velocidad de respuesta, ruido y estabilidad durante el uso.";

            component.ElectricalCharacteristicsExtended =
                $"{component.ElectricalCharacteristics} En una ficha tecnica real estos parametros se separan en valores absolutos maximos y condiciones recomendadas. Los valores absolutos indican limites que no deben superarse; las condiciones recomendadas indican donde el componente trabaja de forma estable. Para una practica segura conviene medir alimentacion, corriente y temperatura, y dejar margen si el circuito se usa con motores, reles, LEDs de potencia, sensores analogicos o comunicacion digital.";

            component.TypicalCircuitExtended =
                $"{component.TypicalCircuit} El circuito tipico debe incluir alimentacion, tierra comun, elemento de proteccion cuando corresponda y puntos de medicion para verificar entrada y salida. Antes de soldar o montar definitivo se recomienda probar la conexion en protoboard, confirmar polaridad, revisar que no haya cortocircuitos y comprobar que el componente cumple su funcion con una carga pequena. Si se usa con microcontrolador, la senal debe respetar niveles logicos y corriente maxima del pin.";

            component.IdentificationExtended =
                $"{component.Identification} Para reconocerlo en fisico se observa forma, serigrafia, color, marca del fabricante, codigo impreso y posicion de pines. En componentes discretos tambien ayuda medir con multimetro; en modulos se revisan etiquetas como VCC, GND, IN, OUT, SIG, SDA, SCL, TX o RX. Si hay dudas, la comparacion con una imagen de referencia y el datasheet evita errores de conexion, especialmente cuando varios encapsulados se parecen.";
        }

        private static void ApplyAdvancedPinOverrides(ComponentInfo component)
        {
            switch (component.Name)
            {
                case "Microcontrolador ATmega328P":
                    component.Pins = "28 pines en DIP: VCC, GND, puertos B/C/D, ADC, RESET, XTAL1 y XTAL2.";
                    component.NominalValues = "1.8 V a 5.5 V segun frecuencia. En Arduino: 5 V y 16 MHz.";
                    component.Identification = "En DIP tiene muesca superior; la numeracion empieza cerca del punto o marca.";
                    break;
                case "ESP32":
                    component.Pins = "GPIO multifuncion: ADC, PWM, SPI, I2C, UART, touch, EN, 3V3, VIN y GND segun placa.";
                    component.NominalValues = "Logica de 3.3 V. Durante WiFi puede superar 300 mA de consumo.";
                    component.Identification = "La serigrafia de la placa indica GPIO, EN, 3V3, VIN y GND.";
                    break;
                case "Raspberry Pi Pico RP2040":
                    component.Pins = "40 pines: GPIO 3.3 V, ADC, UART, SPI, I2C, PWM, VSYS, VBUS, 3V3 y GND.";
                    component.NominalValues = "GPIO de 3.3 V. VSYS aproximado: 1.8 V a 5.5 V segun placa.";
                    component.Identification = "Puerto USB como referencia superior; pines rotulados en los bordes.";
                    break;
                case "Driver L293D":
                    component.Pins = "16 pines: entradas, salidas, enables, VCC logico, VCC motor y tierras.";
                    component.NominalValues = "Logica tipica 5 V; motor hasta decenas de voltios segun disipacion y modelo.";
                    break;
                case "Driver ULN2003":
                    component.Pins = "16 pines: 7 entradas, 7 salidas, COM para diodos clamp y GND.";
                    component.NominalValues = "Entradas TTL/CMOS; salidas para cargas de cientos de mA segun disipacion.";
                    break;
                case "Registro 74HC595":
                    component.Pins = "16 pines: SER, SRCLK, RCLK, OE, MR, Q0-Q7, Q7S, VCC y GND.";
                    component.NominalValues = "Alimentacion comun 2 V a 6 V; salidas para cargas pequenas.";
                    break;
                case "Contador CD4017":
                    component.Pins = "16 pines: clock, reset, enable, carry out, Q0-Q9, VDD y VSS.";
                    component.NominalValues = "Alimentacion CMOS comun 3 V a 15 V segun variante.";
                    break;
                case "Multiplexor CD4051":
                    component.Pins = "16 pines: X0-X7, comun X, selectores A/B/C, INH, VDD, VSS y VEE.";
                    component.NominalValues = "Senales analogicas dentro del rango definido por VEE, VSS y VDD.";
                    break;
                case "Amplificador LM386":
                    component.Pins = "8 pines: gain, entradas, GND, salida, VCC y bypass.";
                    component.NominalValues = "Alimentacion comun 4 V a 12 V; ganancia de 20 a 200.";
                    break;
                case "Driver MAX7219":
                    component.Pins = "24 pines: DIN, CLK, LOAD/CS, segmentos, digitos, ISET, V+ y GND.";
                    component.NominalValues = "Alimentacion comun 5 V; corriente definida por resistencia ISET.";
                    break;
            }
        }

        private static bool Contains(string text, string query)
        {
            return text.Contains(query, StringComparison.OrdinalIgnoreCase);
        }

        private static List<DatasheetLink> BuildDatasheetLinks(string name)
        {
            string encodedName = Uri.EscapeDataString(name);
            string popularTerm = GetPopularDatasheetTerm(name);
            string encodedPopularTerm = Uri.EscapeDataString(popularTerm);

            return
            [
                new DatasheetLink
                {
                    Title = $"{popularTerm} - busqueda en AllDatasheet",
                    Url = $"https://www.alldatasheet.com/view.jsp?Searchword={encodedPopularTerm}"
                },
                new DatasheetLink
                {
                    Title = $"{popularTerm} - resultados en Datasheet4U",
                    Url = $"https://datasheet4u.com/share_search.php?sWord={encodedPopularTerm}"
                },
                new DatasheetLink
                {
                    Title = $"{name} - busqueda tecnica en Mouser",
                    Url = $"https://www.mouser.com/c/?q={encodedName}%20datasheet"
                },
                new DatasheetLink
                {
                    Title = $"{popularTerm} - busqueda en DigiKey",
                    Url = $"https://www.digikey.com/en/products/result?keywords={encodedPopularTerm}"
                },
                new DatasheetLink
                {
                    Title = $"{popularTerm} - referencia en Octopart",
                    Url = $"https://octopart.com/search?q={encodedPopularTerm}"
                },
                new DatasheetLink
                {
                    Title = $"{popularTerm} - modelos y simbolos en SnapEDA",
                    Url = $"https://www.snapeda.com/search/?q={encodedPopularTerm}"
                },
                new DatasheetLink
                {
                    Title = $"{popularTerm} - busqueda en RS Components",
                    Url = $"https://www.rs-online.com/search?query={encodedPopularTerm}"
                },
                new DatasheetLink
                {
                    Title = $"{popularTerm} - Google datasheet PDF",
                    Url = $"https://www.google.com/search?q={encodedPopularTerm}%20datasheet%20pdf"
                }
            ];
        }

        private static string ToDisplayName(string name)
        {
            // Use original casing with spaces, matching filenames in Simulacion folder
            return name;
        }

        private static string ResolveSimulationPath(string componentName)
        {
            var exts = new[] { ".png", ".webp", ".jpg", ".jpeg" };
            string appDir = AppContext.BaseDirectory;

            // Collect candidate files from any 'Simulacion' directory under the app folder
            var candidates = new List<string>();
            try
            {
                // direct folder under appDir
                var directSim = Path.Combine(appDir, "Simulacion");
                if (Directory.Exists(directSim))
                {
                    foreach (var ext in exts)
                        candidates.AddRange(Directory.GetFiles(directSim, "*" + ext, SearchOption.TopDirectoryOnly));
                }

                // search deeper
                foreach (var dir in Directory.EnumerateDirectories(appDir, "Simulacion", SearchOption.AllDirectories))
                {
                    foreach (var ext in exts)
                        candidates.AddRange(Directory.GetFiles(dir, "*" + ext, SearchOption.TopDirectoryOnly));
                }
            }
            catch
            {
                // ignore IO errors and continue
            }

            if (!candidates.Any())
                return string.Empty;

            string target = Normalize(componentName);

            // Build map normalized filename -> path
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var file in candidates)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                var norm = Normalize(name);
                if (!map.ContainsKey(norm))
                    map[norm] = file;
            }

            // exact match
            if (map.TryGetValue(target, out var exact))
                return new Uri(exact).AbsoluteUri;

            // contains or startswith matches
            var contains = map.Keys.FirstOrDefault(k => k.Contains(target));
            if (contains != null)
                return new Uri(map[contains]).AbsoluteUri;

            var starts = map.Keys.FirstOrDefault(k => target.Contains(k));
            if (starts != null)
                return new Uri(map[starts]).AbsoluteUri;

            // best fuzzy: minimal Levenshtein distance (simple heuristic)
            string best = null;
            int bestScore = int.MaxValue;
            foreach (var kv in map)
            {
                int score = LevenshteinDistance(kv.Key, target);
                if (score < bestScore)
                {
                    bestScore = score;
                    best = kv.Value;
                }
            }

            if (best != null && bestScore <= Math.Max(1, target.Length / 4))
                return new Uri(best).AbsoluteUri;

            return string.Empty;
        }

        private static string MakeRelative(string baseDir, string fullPath)
        {
            try
            {
                var rel = Path.GetRelativePath(baseDir, fullPath).Replace('\\', '/');
                return rel;
            }
            catch
            {
                return fullPath.Replace('\\', '/');
            }
        }

        private static string Normalize(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;
            s = s.ToLowerInvariant();
            s = s.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in s)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc == UnicodeCategory.NonSpacingMark)
                    continue;
                if (char.IsLetterOrDigit(ch))
                    sb.Append(ch);
            }
            return sb.ToString();
        }

        private static int LevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a)) return b?.Length ?? 0;
            if (string.IsNullOrEmpty(b)) return a.Length;
            int[,] d = new int[a.Length + 1, b.Length + 1];
            for (int i = 0; i <= a.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= b.Length; j++) d[0, j] = j;
            for (int i = 1; i <= a.Length; i++)
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            return d[a.Length, b.Length];
        }

        private static string GetPopularDatasheetTerm(string name)
        {
            return name switch
            {
                "Resistencia" => "Vishay metal film resistor",
                "Capacitor ceramico" => "Murata ceramic capacitor",
                "Capacitor electrolitico" => "Nichicon electrolytic capacitor",
                "Inductor" => "Bourns power inductor",
                "Potenciometro" => "Bourns potentiometer",
                "Trimmer" => "Bourns trimmer potentiometer",
                "Termistor NTC" => "NTC thermistor 10K",
                "LDR" => "GL5528 LDR",
                "Fusible" => "Littelfuse fuse",
                "Cristal de cuarzo" => "16MHz crystal HC49",
                "Diodo rectificador" => "1N4007",
                "LED" or "LED indicador" => "Kingbright LED 5mm",
                "Diodo Zener" => "1N4733A zener",
                "Transistor BJT NPN" => "2N2222",
                "Transistor BJT PNP" => "2N3906",
                "MOSFET canal N" => "IRLZ44N",
                "Optoacoplador" => "PC817",
                "Regulador 7805" => "LM7805",
                "Amplificador operacional" => "LM358",
                "Circuito integrado 555" => "NE555",
                "Microcontrolador ATmega328P" => "ATmega328P",
                "ESP32" => "ESP32 WROOM 32",
                "Raspberry Pi Pico RP2040" => "RP2040",
                "Driver L293D" => "L293D",
                "Driver ULN2003" => "ULN2003",
                "Registro 74HC595" => "74HC595",
                "Contador CD4017" => "CD4017",
                "Multiplexor CD4051" => "CD4051",
                "Amplificador LM386" => "LM386",
                "Driver MAX7219" => "MAX7219",
                "Sensor ultrasonico HC-SR04" => "HC-SR04",
                "Sensor DHT11" => "DHT11",
                "Sensor de temperatura LM35" => "LM35",
                "Sensor de gas MQ-2" => "MQ-2 gas sensor",
                "Pantalla OLED I2C" => "SSD1306 OLED",
                "Pantalla LCD 16x2" => "HD44780 LCD",
                "Servomotor" => "SG90 servo",
                "Motor DC" => "DC motor datasheet",
                "Modulo cargador TP4056" => "TP4056",
                "Regulador buck LM2596" => "LM2596",
                "Convertidor boost MT3608" => "MT3608",
                _ => $"{name} datasheet"
            };
        }

        private static string ToAssetName(string name)
        {
            char[] chars = name
                .ToLowerInvariant()
                .Select(character => char.IsLetterOrDigit(character) ? character : '-')
                .ToArray();

            return string.Join("-", new string(chars).Split('-', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
