#imports all required libs
import json
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
from numpy.polynomial.polynomial import polyfit
import seaborn as sns

#style
sns.set_theme(style="darkgrid")

#load data
file = open('Assets/Data/Day_5.json')
data = pd.read_json(file)

#edit to change what data you want
xAxis = 'm_geneticMoveSpeed'
yAxis = 'm_curFitness'

#takes data and puts in list
xAxisData = [i[xAxis] for i in data['Day_5']]
yAxisData = [i[yAxis] for i in data['Day_5']]

#prints to console for debug
df = pd.DataFrame({ xAxis[2:]:xAxisData, yAxis[2:]:yAxisData})
print(df.sort_values(by=yAxis[2:]))

#parsing data to graph
g = sns.regplot(x = xAxisData,
                y = yAxisData,
                data = data,
                robust=True)
plt.show()
