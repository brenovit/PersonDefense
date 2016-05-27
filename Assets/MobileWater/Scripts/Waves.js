#pragma strict
 
 var scale : float = 10.0;
 var speed : float = 1.0;
 var power : float = .3f;

 private var mesh :Mesh;
 private var height : Vector3[];
 
 function Awake(){ 
  	mesh  = GetComponent(MeshFilter).mesh;  
 }
 
 function Update () {
       
  
         if (height == null)
                 height = mesh.vertices;
  
         var vertices : Vector3[] = new Vector3[height.Length];
         for (var i=0;i<vertices.Length;i++)
         {
                 var vertex = height[i];
                 vertex.y += Mathf.Sin(Time.time * speed+ height[i].x + height[i].y + height[i].z) * scale;
                 vertex.y += Mathf.PerlinNoise(height[i].x , height[i].y + Mathf.Sin(Time.time * 0.1)    ) * power;
                 vertices[i] = vertex;
         }
         mesh.vertices = vertices;
         mesh.RecalculateNormals();
 }
