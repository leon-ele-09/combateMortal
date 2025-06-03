import cv2

from cvzone.PoseModule import PoseDetector
import socket



width = 1280 / 2
height = 720 / 2
# Camarita

cap = cv2.VideoCapture(0)


cap.set(3, 1280 / 2)

cap.set(4, 720 / 2)

# Detector

detector = PoseDetector(detectionCon=0.8)


sockHand = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 2000)


while True:
    # Frame de camara
    success, img = cap.read()

    # Deteccion de pose
    img = detector.findPose(img)

    # Posición de landmarks
    lmList, bboxInfo = detector.findPosition(img, bboxWithHands=False)
    if bboxInfo:
        center = bboxInfo["center"]
        cv2.circle(img, center, 5, (255, 0, 255), cv2.FILLED)

    data = []
    # Landmarks -> (x, y, z) * 21 (63 values)
    for lm in lmList:
        # Se ajusta la posicion en y
        data.extend([lm[0], height - lm[1], lm[2]])

    sockHand.sendto(str.encode(str(data)), serverAddressPort)
    
    cv2.imshow("Image", img)
    cv2.waitKey(1)
    if cv2.getWindowProperty("Image", cv2.WND_PROP_VISIBLE) < 1:
        print("Window closed")
        break

sockHand.close()
