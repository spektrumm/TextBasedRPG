import json

tempList = []
tupleList = []
saveData = {'playerName':'null',
            'playerExp':0,
            'currentScene':'null',
            'weaponName':'null',
            'resourceAmt':0,
            'playerClass':'null'}

with open('tempSave.txt', 'r') as tempRead:
    outputLines = tempRead.readlines()
    endList = []
    splitList = []
    n = 0
    i = 0
    j = 0
    
    for lines in outputLines:
        if n < (len(outputLines) - 1):
            size = len(outputLines[n])
            ogString = outputLines[n]
            modString = ogString[:size - 1]
            endList.append(modString)
        else:
            size = len(outputLines[n])
            ogString = outputLines[n]
            endList.append(ogString)
        n += 1

    for items in endList:
        splitList = items.split(":")
        tupleList.append(splitList)

    for values in tupleList:
        if values[0] == 'playerName':
            saveData.update({'playerName': values[1]})
        elif values[0] == 'playerExp':
            saveData.update({'playerExp': int(values[1])})
        else:
            pass


    print(saveData)