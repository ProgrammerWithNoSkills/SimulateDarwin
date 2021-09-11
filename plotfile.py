import json
import pandas as pd
import matplotlib.pyplot as plt
f = open('C:/Users/John/Desktop/Projects/JinMingSciProject2021/SimulateDarwin/Assets/Data/Day_5.json',)
data = json.load(f)
xAxisData = 'm_geneticMoveSpeed'
yAxisData = 'm_curFitness'
xAxis = [i[xAxisData] for i in data["Day_5"]]
yAxis = [i[yAxisData] for i in data['Day_5']]

df = pd.DataFrame({ xAxisData[2:]:xAxis, yAxisData[2:]:yAxis })

#print(df.sort_values(by='xAxis'))

ax = df.plot()
ax.set_title(xAxisData[2:] + ' vs ' + yAxisData[2:], fontsize=20)
ax.set_xlabel(xAxisData[2:])
ax.set_ylabel(yAxisData[2:])
#plt.scatter(xAxis, yAxis)
plt.show()
