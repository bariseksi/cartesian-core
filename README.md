# cartesian-core
.net library that translates any x,y data to pixel coordinates 
<p align="left">
  <img src="images/plane.png" width="1000">
</p>

## methods
### CartesianCore.Plane
class constructor
```c#
public Plane(int width, int height, int topMargin = 0, int bottomMargin = 0, int leftMargin = 0, int rightMargin = 0)
```
initializes a new plane 
> **width**: width of the area plane to be drawn (pixel)<br>
> **height**: height of the area plane to be drawn (pixel)<br>
> **topMargin**: margin from top to the plane (pixel)<br>
> **bottomMargin**: margin from bottom to the plane (pixel)<br>
> **leftMargin**: margin from left to plane (pixel)<br>
> **rightMargin**: margin from right to plane (pixel)<br>

### CartesianCore.Dataset
class constructor
```c#
public Dataset(int datasetID)
```
initializes a new dataset<br>
represents a set of data in (x,y) pairs
>**datasetID**: id of the dataset to be used while getting translated coordinates from instance of a plane class.<br>
Ex: plane.GetDataset(int datasetID)
