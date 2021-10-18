#Python Script to iterate through CombinedData to get wanted fields
import json

with open('Assets/Data/Combined_Data/CombinedData.json') as file:
    dataSet = json.load(file)
    file.close()

speciesPops = {}

for Day in dataSet:
    speciesPops.update({Day: {}})
    RedCreaturePopInDay = 0
    BlueCreaturePopInDay = 0
    for creatureInfo in dataSet[Day]: #for each creature info in 'Day_x' list
        #isRed = False
        #isBlue = False
        for Key in creatureInfo: #For key in dict creatureInfo
            if Key == "m_speciesID" and creatureInfo[Key] == 1:
                #isRed = True
                RedCreaturePopInDay += 1
            elif Key == "m_speciesID" and creatureInfo[Key] == 2:
                #isBlue = True
                BlueCreaturePopInDay += 1
            #if Key == "m_curFitness" and creatureInfo[Key] > 0 and isRed == True:
                #RedCreaturePopInDay += 1
            #elif Key == "m_curFitness" and creatureInfo[Key] > 0 and isBlue == True:
                #BlueCreaturePopInDay += 1
        speciesPops[Day]["RedCreaturePopulation"] = RedCreaturePopInDay
        speciesPops[Day]["BlueCreaturePopulation"] = BlueCreaturePopInDay

print(speciesPops)
