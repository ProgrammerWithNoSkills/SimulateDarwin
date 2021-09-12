#imports all required libs
import json
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
from numpy.polynomial.polynomial import polyfit
import seaborn as sns
sns.set_theme(style="darkgrid")

#load data
file = open('C:/Users/John/Desktop/Projects/JinMingSciProject2021/SimulateDarwin/Assets/Data/Day_15.json')
data = pd.read_json(file)

#edit to change what data you want
xAxis = 'm_geneticMoveSpeed'
yAxis = 'm_curFitness'

#takes data and puts in list
xAxisData = [i[xAxis] for i in data['Day_15']]
yAxisData = [i[yAxis] for i in data['Day_15']]

#parsing data to graph
df = pd.DataFrame({ xAxis[2:]:xAxisData,
                    yAxis[2:]:yAxisData})

#prints to console for debug
print(df.sort_values(by=yAxis[2:]))
g = sns.regplot(x= xAxisData, y= yAxisData,data=data)
plt.show()
