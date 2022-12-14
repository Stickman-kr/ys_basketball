import cv2
import mediapipe as mp
import time
import socket
import math
import numpy as np

class handDetector():
    def __init__(self, mode=False, maxHands=2, complexity = 1, detectionCon =0.5, trackCon = 0.5):
        self.mode = mode
        self.maxHands = maxHands
        self.complexity = complexity
        self.detectionCon = detectionCon
        self.trackCon = trackCon

        self.mpHands = mp.solutions.hands
        self.hands = self.mpHands.Hands(self.mode, self.maxHands,self.complexity,
                                        self.detectionCon, self.trackCon)
        self.mpDraw = mp.solutions.drawing_utils


    def findHands(self,img, draw = True):
        imgRGB = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
        self.results = self.hands.process(imgRGB)
        #print(results.multi_hand_landmarks)
        if self.results.multi_hand_landmarks:
            for handLms in self.results.multi_hand_landmarks:
                if draw:
                    self.mpDraw.draw_landmarks(img, handLms, self.mpHands.HAND_CONNECTIONS)
        return img

    def findPosition(self,img,handNo =0,draw = True):
        lmList = []
        if self.results.multi_hand_landmarks:
            myHand = self.results.multi_hand_landmarks[handNo]
            for id, lm in enumerate(myHand.landmark):
                #print(id,lm)
                h, w, c = img.shape
                cx, cy = int(lm.x * w), int(lm.y * h)  # x,y 위치 체크
                #print(id, cx, cy)
                lmList.append([cx,cy])
                if draw:
                    if handNo == 0:
                        cv2.circle(img, (cx, cy), 15, (150, 150, 255), cv2.FILLED)  # 점 20 개중에서 0개 선택
                    else:
                        cv2.circle(img, (cx, cy), 15, (150, 255, 0), cv2.FILLED)  # 두번째 손 체크
        return lmList

#communication

HOST ="127.0.0.1"
PORT = 9999
server_socket = socket.socket(socket.AF_INET,socket.SOCK_STREAM) #TCP
server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server_socket.bind((HOST, PORT))
server_socket.listen()
client_socket, addr = server_socket.accept()
print('Connected by', addr)

# distance measure

x = [300, 245, 200, 170, 145, 130, 112, 103, 93, 87, 80, 75, 70, 67, 62, 59, 57]
y = [20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100]
coff = np.polyfit(x, y, 2)

def calc_z(lmList,coff = coff):
    A, B, C = coff
    x1, y1 = lmList[5]
    x2, y2 = lmList[17]
    distance = int(math.sqrt((y2 - y1) ** 2 + (x2 - x1) ** 2))
    distanceCM = A * distance ** 2 + B * distance + C
    return distanceCM

def main():
    pTime = 0
    cTime = 0
    cap = cv2.VideoCapture(0)
    width, height = 1280, 720
    cap.set(3,width)
    cap.set(4,height)
    detector = handDetector()
    while True:
        data = []
        success, img = cap.read()

        img = detector.findHands(img)
        lmList = detector.findPosition(img,handNo=0)
        if lmList:
            try:
                lmList2 = detector.findPosition(img,handNo=1)
                if len(lmList2) != 0:
                    z1 = int(calc_z(lmList2))
                    cv2.putText(img,'hand1:'+str(z1)+'cm', (400, 70), cv2.FONT_HERSHEY_PLAIN, 2, (0, 0, 255), 2)
                    for lm in lmList2:
                        data.extend([lm[0], height - lm[1], z1])
            except:
                pass
        if len(lmList) != 0:
            z2 = int(calc_z(lmList))
            cv2.putText(img, 'hand2:'+str(z2)+'cm', (400, 140), cv2.FONT_HERSHEY_PLAIN, 2, (0, 0, 255), 2)
            for lm in lmList:
                data.extend([lm[0], height -lm[1],z2])
        client_socket.send(str.encode(str(data)))


        # current time
        cTime = time.time()
        fps = 1 / (cTime - pTime)
        pTime = cTime

        # fps calculate
        cv2.putText(img, str(int(fps)), (10, 70), cv2.FONT_HERSHEY_PLAIN, 3, (0, 0, 255), 3)
        img = cv2.resize(img,(0,0),None,0.5,0.5)
        cv2.imshow("Image", img)
        cv2.waitKey(1)

if __name__ == "__main__":
    main()