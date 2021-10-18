#imports all required libs
import json
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
from numpy.polynomial.polynomial import polyfit
import seaborn as sns

#style
sns.set(rc={'axes.facecolor':'silver',
                'figure.facecolor':'silver'})
#colours
BCB_Blue = '#00F2FE'
BCB_Green = '#02EF69'
BCB_Red = '#FE0000'

#load data
file = open('Assets/Data/Day_5.json')
data = pd.read_json(file)

#edit to change what data you want
xAxis = 'm_geneticMoveSpeed'
yAxis = 'm_curFitness'
#for comparing
zAxis = 'm_geneticSightRange'

#takes data and puts in list
xAxisData = [i[xAxis] for i in data['Day_5']]
yAxisData = [i[yAxis] for i in data['Day_5']]
zAxisData = [i[zAxis] for i in data['Day_5']]

#prints to console for debug
df = pd.DataFrame({ xAxis[2:]:xAxisData, yAxis[2:]:yAxisData})
print(df)
#print(df.sort_values(by=yAxis[2:]))

#graph options
ax1 = plt.bar(xAxisData, yAxisData)

#ax2 = ax1.twiny()

#ax2 = sns.regplot(x=zAxisData, y=yAxisData, data=data,
                #fit_reg=True, ci=95, truncate=True,
                #marker="x", label=zAxis[2:], color=BCB_Green)

#legend
#ax1.legend(loc=1)
#ax2.legend(loc=1)

#axis and title
#ax1.set_xlabel(xAxis[2:])
#ax2.set_xlabel(zAxis[2:])
plt.title('Day 25 Populations', size=16, font='Verdana',
          fontstyle='italic', weight='bold', c='black')


plt.show()
