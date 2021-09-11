import json
import pandas as pd
import matplotlib.pyplot as plt
f = open('C:/Users/John/Desktop/Projects/JinMingSciProject2021/SimulateDarwin/Assets/Data/Day_35.json',)
data = json.load(f)
xAxisData = 'm_geneticMoveSpeed'
yAxisData = 'm_curFitness'
xAxis = [i[xAxisData] for i in data["Day_35"]]
yAxis = [i[yAxisData] for i in data['Day_35']]

#df = pd.DataFrame({ xAxisData[2:]:xAxis, yAxisData[2:]:yAxis })
df = pd.DataFrame({ 'Speed':xAxis, 'Fitness':yAxis })
#print(df.sort_values(by='xAxis'))

ax = df.plot()
ax.set_title(xAxisData[2:] + ' vs ' + yAxisData[2:], fontsize=20)
ax.set_xlabel(xAxisData[2:])
ax.set_ylabel(yAxisData[2:])
ax.set_ylim([0, 15])
ax.set_xlim([0.5, 30])
#plt.scatter(xAxis, yAxis)
plt.show()
