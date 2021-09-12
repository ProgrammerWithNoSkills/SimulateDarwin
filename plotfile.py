#imports all required libs
import json
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
from numpy.polynomial.polynomial import polyfit

#load data
f = open('C:/Users/John/Desktop/Projects/JinMingSciProject2021/SimulateDarwin/Assets/Data/Day_10.json',)
data = json.load(f)

#edit to change what data you want
xAxisData = 'm_geneticMoveSpeed'
yAxisData = 'm_curFitness'

#takes data and puts in list
xAxis = [i[xAxisData] for i in data['Day_10']]
yAxis = [i[yAxisData] for i in data['Day_10']]

#parsing data to graph
df = pd.DataFrame({ xAxisData[2:]:xAxis,
                    yAxisData[2:]:yAxis})

#best fit line calcs
bestFitX = np.array(xAxis)
bestFitY = np.array(yAxis)
m, b = np.polyfit(bestFitX, bestFitY, 1)

#prints to console for debug
print(df.sort_values(by=yAxisData[2:]))

#graph shit
ax = df.plot.scatter(x=xAxisData[2:], y=yAxisData[2:])
ax.set_title(xAxisData[2:] + ' vs ' + yAxisData[2:], fontsize=20)
ax.set_xlabel(xAxisData[2:])
ax.set_ylabel(yAxisData[2:])

#draw best fit
plt.plot(bestFitX, bestFitY, '.')
plt.plot(bestFitX, m*bestFitX + b, '-')
plt.show()
