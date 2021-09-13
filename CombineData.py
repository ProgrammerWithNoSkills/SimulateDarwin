#Python program to take a list of json files and combine them into one file

import json, os
from pathlib import Path

days = 0 #change for number of days of data to combine\

if not os.path.exists('./Assets/Data/Combined_Data/'):
    os.makedirs('./Assets/Data/Combined_Data/')

combinedDataFile = open("./Assets/Data/Combined_Data/" + "CombinedData.json", "w")
combinedDataDict = {}

for dataFile in sorted(Path('./Assets/Data').glob('*.json'), key=lambda path: int(path.stem.rsplit("_")[1])):
    if dataFile.is_file() and str(dataFile).endswith(".json") and os.path.basename(dataFile) != "CombinedData.json":
        dayFile = open(dataFile, "r")
        jsonDayFile = json.load(dayFile)
        combinedDataDict.update(jsonDayFile)
        dayFile.close()

        print(os.path.basename(dataFile) + " appended to Combined Dict")
        print()
        days += 1

combinedDataFile.write(json.dumps(combinedDataDict, indent = 4))
print("Days 1 to {0} appended to /Assets/Data/Combined_Data/CombinedData.json succesfully.".format(days))
