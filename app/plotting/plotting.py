import json
import matplotlib.pyplot as plt
import matplotlib.patches as patches
import sys
import os
try:
	fileName = sys.argv[1]
except IndexError:
	print("You must specify a file path")
	
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
	rect = patches.Rectangle((minX, minY), maxX - minX, maxY - minY, linewidth=1, edgecolor='r', facecolor='none')
	ax.add_patch(rect)
	
plot(rootNode)
ax.scatter(xs, ys)
plt.show()