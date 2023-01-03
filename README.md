# cartesian-core
.net library that translates any x,y data to pixel coordinates 
- **CartesianCore** the library itself.
- **CartesianCanvasDrawer** an example project that draws graph on a canvas.
- **CartesianBitmapDrawer** an example project that draws graph on a bitmap.
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

### CartesianCore.Plane.AddDataset
adds dataset to a plane to be processed and sets plane's maximum & minimum values of each x,y axis.
```c#
public void AddDataset(Dataset dataset)
```
> **dataset**: an instance of a Dataset class

### CartesianCore.Plane.SetAxes
Sets plane's maximum & minimum values of each x,y axis and makes plane ignores calculated maximum & minimum values of each axis from added datasets<br>
This can be called before ``` public void AddDataset(Dataset dataset)```
```c#
public void SetAxes(double xMin, double xMax, double yMin, double yMax)
```
> **xMin**: minimum value of X axis<br>
> **xMax**: maximum value of X axis<br>
> **yMin**: minimum value of Y axis<br>
> **yMax**: maximum value of Y axis<br>

### CartesianCore.Dataset
class constructor
```c#
public Dataset(int datasetID)
```
initializes a new dataset<br>
represents a set of data in (x,y) pairs
>**datasetID**: id of the dataset to be used while getting translated coordinates from instance of a plane class.<br>
Ex: plane.GetDataset(int datasetID)
