

import os
import re 
import json


'''
Adding a field to all the json files 
May not work on Mac and Linux 
'''

FILE_TYPES = ["json"]
ADD_KEY = "MysteryDoors"
ADD_VALUE = [False, False, False, False]

DETAILING = True 
APPEND_PATH = True

class JsonFile:
    def __init__(self, inName = None, inExt = None):
        self.__extension = inExt;
        self.__name = inName;
        self.__path = "";

    def setName(self, inName):
        self.__name = inName;

    def setExtension(self, inExt):
        self.__extension = inExt;

    def setPath(self, inPath):
        self.__path = inPath; 

    def getName(self):
        return self.__name;

    def getExt(self):
        return self.__extension;

    def fullPath(self):
        return self.__path + "\\" + self.__name + self.__extension;

    def toString(self):
        return self.__name + self.__extension;


def getCurrentFolder():
    current = str(os.path.abspath(__file__))
    for itera in range(len(current) - 1, 0, -1):
        if current[itera] == '\\':
            dir = current[0: itera]#Get current directory
            break;
    return dir


def findType(tarTypes = FILE_TYPES):

    fileList = os.listdir(getCurrentFolder())

    JsonFiles = []

    for f in fileList:
        for ext in tarTypes:
            if f.lower()[-len(ext):] == ext:
                NewFile = JsonFile(f[:len(f) - len(ext)], ext)

                if APPEND_PATH:
                    NewFile.setPath(getCurrentFolder());

                JsonFiles.append(NewFile)

                # Debugging info 
                if DETAILING: print(NewFile.fullPath()) 
                

    return JsonFiles

def checkList(input):
    output = ""
    if isinstance(input, list):
        output = checkList(input)
    else:
        return str(out)

def BatchEdit(FileList):

    for SingleFile in FileList:
        with open(SingleFile.fullPath(), 'r+') as f:

            strData = f.read()
            index = 0 

            for char in strData:
                if char == "\"" or char == "{": break 
                index += 1 
            strData = "{" + strData[index:] if index > 0 else strData

            if DETAILING: print(strData)

            jsonData = json.loads(strData)
            if ADD_KEY not in jsonData:
                jsonData[ADD_KEY] = ADD_VALUE 

            if DETAILING: print("NEW VER \n\n", jsonData)

            f.truncate(0)

        with open(SingleFile.fullPath(), 'w') as f:
            json.dump(jsonData, f)
            

def Main():
    try:
        waitingToAdd = findType()
        BatchEdit(waitingToAdd)
    except (ValueError,NameError,SyntaxError,AttributeError,\
        TypeError) as err:
        print('Error!',err);

if __name__ == "__main__":
    Main()
