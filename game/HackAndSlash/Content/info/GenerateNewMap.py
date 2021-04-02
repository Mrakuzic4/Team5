

import os
import re 
import json

FILL_ELEMENT = 0
COLUMN = 12
ROW = 7 
TESTING = True 

template = {
    "Arrangement" : [],
    "DropPotions": 0,
    "DropKeys": 0,
    "LockedDoors": [ False, False, False, False ],
    "OpenDoors": [ False, False, False, False ],
    "HiddenDoors": [ False, False, False, False ],
    "MysteryDoors": [ False, False, False, False ],
    "DefaultBlock": 0
    }


def Generate():
    instance = template.copy()

    for i in range(ROW):
        instance["Arrangement"].append([])
        for j in range(COLUMN):
            instance["Arrangement"][i].append(FILL_ELEMENT)

    if TESTING: print(instance["Arrangement"])

    return instance 
    
def saveSingle(fileName):
    instance = Generate()

    with open(fileName, "w") as f:
        json.dump(instance, f)
    
            

def Main():
    try:
        saveSingle("temp.json")

    except (ValueError,NameError,SyntaxError,AttributeError,\
        TypeError) as err:
        print('Error!',err);

if __name__ == "__main__":
    Main()
