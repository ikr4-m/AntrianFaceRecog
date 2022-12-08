import face_recognition
import pickle
import os

FOLDER_PATH = 'dlibsample'
FACE_JITTER = 20
FILE_NAME = 'dataset_faces.dat'

first_name_list = [x for x in os.listdir(FOLDER_PATH) if os.path.isdir(f'./{FOLDER_PATH}/{x}')]
all_face_encodings = {}
print(f'Get {len(first_name_list)} group name.')
for x in first_name_list:
    file_path = f'./{FOLDER_PATH}/{x}'
    file_list = [f'{file_path}/{xx}' for xx in os.listdir(file_path) if os.path.isdir(f'{file_path}/{xx}') is False]
    print(f'Get {x} name with {len(file_list)} face.')
    for image in file_list:
        key_name = f'{x}:{image.split("/").pop().split(".")[0]}'
        print(f'Resampling {key_name} with {FACE_JITTER} times')
        image = face_recognition.load_image_file(image)
        all_face_encodings[key_name] = face_recognition.face_encodings(image, num_jitters=FACE_JITTER)[0]

print(f'Save the dataset to {FILE_NAME}')
with open(FILE_NAME, 'wb') as f:
    pickle.dump(all_face_encodings, f)
print('Done!')