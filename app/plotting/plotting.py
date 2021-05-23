import json
import matplotlib.pyplot as plt
import matplotlib.patches as patches
import sys
import os
from random import uniform

try:
	fileName = sys.argv[1]
except:
	print("You must specify a file path")
	sys.exit(1)

try:
    depth = sys.argv[2]
except:
    depth = None
    pass

		
if not os.path.exists(fileName):
	raise ValueError("File does not exist")
	

with open(fileName) as f:
	data = json.load(f)
xs = []
ys = []
rootNode = data["rootNode"]
print(rootNode)
fig, ax = plt.subplots()

def plot(object):
	print("plotting bbox")
	bbox = object['bbox']
	if (depth == None or object['depth'] == int(depth)):
		plotBBox(bbox['minX'],bbox['minY'],bbox['maxX'],bbox['maxY'])

	if (object['childNodes'] != []):
		for object in object['childNodes']:
			plot(object)
		return
		
	elif (object['geometries']!=[]): 
		print(f"Plotting point geometries for {object['id']}")
		for point in object['geometries']:
			
			xs.append(point["x"])
			ys.append(point["y"])
		return
		
		
		
		
def plotBBox(minX,minY,maxX,maxY):
	rect = patches.Rectangle((minX, minY), maxX - minX, maxY - minY, linewidth=1, edgecolor=(uniform(0, 1), uniform(0, 1), uniform(0, 1)), facecolor='none')
	ax.add_patch(rect)
	
plot(rootNode)
ax.scatter(xs, ys)
plt.show()
