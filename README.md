# 🥋 Kombate Mortal

**Kombate Mortal** es un juego de combate en 2D con integración de control por gestos mediante visión computacional. Usa `MediaPipe` para capturar poses humanas en tiempo real y traducirlas en acciones dentro del juego, ofreciendo una experiencia de combate interactiva y sin mando.

---

## 🧠 Características principales

- 🎮 **Control por gestos** con MediaPipe Pose.
- 🕹️ Alternativa de controles por teclado (modo manual).
- 🤺 **4 poses personalizadas** que activan diferentes acciones en el juego.
- 🌄 Escenarios en 3D con **iluminación dinámica**.
- 🧍‍♂️ Sprites 2D personalizados para los personajes.
- 🔊 Efectos visuales y sonoros al ejecutar ataques y defensas.

---

## 📦 Requisitos

- Python 3.9 o superior
- OpenCV (cv2)
- MediaPipe


## 🧍‍♂️ Control por Poses (MediaPipe)

El sistema de control por visión se basa en MediaPipe Pose, que detecta 33 puntos clave del cuerpo en tiempo real usando la cámara web. Se definen cuatro poses principales que corresponden a movimientos dentro del juego:
| Pose                                                | Accion de juego    |
|-----------------------------------------------------|--------------------|
| Brazo derecho al frente                             | Ataque Ligero      |
| Brazo izquierdo al frente                           | Ataque Pesado      |
| Ambos brazos al frente                              | Esquivar           |
| Brazos abiertos al frente (Piensa en el kamehameha) | Ataque a distancia |

Estas poses son procesadas mediante un sistema de reconocimiento basado en la comparación de ángulos entre articulaciones.

## 🧑‍💻 Controles manuales (teclado)

En caso de no utilizar la cámara, puedes jugar usando el teclado:

- J → Ataque ligero

- K → Ataque pesado

- L → Esquivar

- U → Ataque de energía

## 🎨 Diseño Visual

### Sprites hechos a mano para los personajes.

### Escenarios en 3D renderizados con luces y sombras que reaccionan con los personajes.

## 🚀 Cómo iniciar

### Asegúrate de tener una cámara conectada si deseas usar los controles por pose.

Ejecuta el archivo principal del juego:

python main.py

Inicia el juego!
