import face_recognition
import cv2
import os
import numpy as np

# Global variable
FOLDER_PATH = 'dlibsample'
FRAME_SKIPPED = 25
FACE_JITTER = 3
SAFE_FRAME_COUNT = 20
DUMP_FACE_NAME = {}
FACE_NAME_DETECTED = []

# Ambil encode image dan list nama baru
first_name_list = [x for x in os.listdir(FOLDER_PATH) if os.path.isdir(f'./{FOLDER_PATH}/{x}')]
name_list = []
encode_image = []
print(f'Get {len(first_name_list)} group name.')
for x in first_name_list:
    file_path = f'./{FOLDER_PATH}/{x}'
    file_list = [f'{file_path}/{xx}' for xx in os.listdir(file_path) if os.path.isdir(f'{file_path}/{xx}') is False]
    print(f'Get {x} name with {len(file_list)} face.')
    for image in file_list:
        print(f'Resampling {x}:{image.split("/").pop().split(".")[0]} with {FACE_JITTER} times')
        image = face_recognition.load_image_file(image)
        encode_image.append(face_recognition.face_encodings(image, num_jitters=FACE_JITTER)[0])
        name_list.append(x)
print(f'Loaded {(sum([x.nbytes for x in encode_image]) / 8) / 1000} MB of image.')

# Initialize some variables
video = cv2.VideoCapture(0)
face_locations = []
face_encodings = []
face_names = []
frame_count = 0
print('Spawning CV2')

def verify_face(name: str) -> any:
    if name == 'Unknown': return
    if name in FACE_NAME_DETECTED: return

    if name not in DUMP_FACE_NAME: DUMP_FACE_NAME[name] = 1
    else: DUMP_FACE_NAME[name] += 1
    if DUMP_FACE_NAME[name] == SAFE_FRAME_COUNT:
        FACE_NAME_DETECTED.append(name)
        del DUMP_FACE_NAME[name]

while True:
    ret, frame = video.read()
    frame_count += 1

    # Proses tiap FRAME_SKIPPED
    if frame_count % FRAME_SKIPPED == 0:
        small_frame = cv2.resize(frame, (0, 0), fx=0.25, fy=0.25)

        # Convert the image from BGR color (which OpenCV uses) to RGB color (which face_recognition uses)
        rgb_small_frame = small_frame[:, :, ::-1]
        
        # Find all the faces and face encodings in the current frame of video
        face_locations = face_recognition.face_locations(rgb_small_frame)
        face_encodings = face_recognition.face_encodings(rgb_small_frame, face_locations)

        face_names = []
        for face_encoding in face_encodings:
            matches = face_recognition.compare_faces(encode_image, face_encoding, tolerance=0.45)
            name = "Unknown"

            # Use the known face with the smallest distance to the new face
            face_distances = face_recognition.face_distance(encode_image, face_encoding)
            best_match_index = np.argmin(face_distances)
            if matches[best_match_index]:
                name = name_list[best_match_index]

            face_names.append(name)

    # Display the results
    for (top, right, bottom, left), name in zip(face_locations, face_names):
        if name == "Unknown": continue
        if name in FACE_NAME_DETECTED: continue

        top *= 4
        right *= 4
        bottom *= 4
        left *= 4

        cv2.rectangle(frame, (left, top), (right, bottom), (0, 0, 255), 2)
        cv2.rectangle(frame, (left, bottom - 35), (right, bottom), (0, 0, 255), cv2.FILLED)
        font = cv2.FONT_HERSHEY_DUPLEX
        cv2.putText(frame, name, (left + 6, bottom - 6), font, 1.0, (255, 255, 255), 1)
        verify_face(name)

    cv2.imshow('Video', frame)
    print(FACE_NAME_DETECTED)

    if cv2.waitKey(1) & 0xFF == ord('q'): break

video.release()
cv2.destroyAllWindows()