import cv2

from cvzone.PoseModule import PoseDetector

# Sending data - UTP protocol
import socket

def find_connected_cameras():
    available_cameras = []
    for i in range(3):  # Probar hasta 10 cámaras
        cap = cv2.VideoCapture(i)
        if cap.isOpened():
            available_cameras.append(i)
            cap.release()  # Liberar la cámara
    print(available_cameras)
    return available_cameras


width = 1280 / 2
height = 720 / 2
# Camarita

cap = cv2.VideoCapture(0)


cap.set(3, 1280 / 2)

cap.set(4, 720 / 2)

# Detector

detector = PoseDetector(detectionCon=0.8)

# Communication
sockHand = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 2000)
serverAddressPort2 = ("127.0.0.1", 4421)

while True:
    # Get the camera frame
    success, img = cap.read()

    # Detecting hand
    # Returns the hand data and the img
    img = detector.findPose(img)

    # We are sending the "Landmark" to data, which is going to reset
    lmList, bboxInfo = detector.findPosition(img, bboxWithHands=False)
    if bboxInfo:
        center = bboxInfo["center"]
        cv2.circle(img, center, 5, (255, 0, 255), cv2.FILLED)

    data = []
    # Landmarks -> (x, y, z) * 21 (63 values)
    for lm in lmList:
        # We do the height transformation because unity uses height coordinates the other way around
        data.extend([lm[0], height - lm[1], lm[2]])

    # print(data)
    sockHand.sendto(str.encode(str(data)), serverAddressPort)
    #sockHand.sendto(str.encode(str(data)), serverAddressPort2)

    cv2.imshow("Image", img)
    cv2.waitKey(1)
    if cv2.getWindowProperty("Image", cv2.WND_PROP_VISIBLE) < 1:
        print("Window closed")
        break

sockHand.close()