using System;

class HW4Controller{
    static void Main(string[] args)
    {
        //Setup the camera
        Camera c2 = new Camera(Camera.Projection.Perspective,
        new Vector(1.0f, 1.0f, 3.0f),
        new Vector(1.0f, 1.0f, 2.0f),
        new Vector(0.0f, 1.0f, 0.0f),
        0.1f, 150f, 512, 512, -7f, 7f, -7f, 7f);

        //Build the scene
        Scene scene = new Scene();
        
        Shape lightsaber = new Lightsaber(new Vector(1,0.5f,2), new Vector(0,1,0), 2, 0.5f);
        lightsaber.Emit = new Vector(100, 100, 255);
        lightsaber.E = true;
        scene.AddShape(ref lightsaber);
        
        Shape s = new Sphere(new Vector(-3.0f, 2.0f, 0.0f), 2f);
        s.D  = new Vector(255, 0, 0);
        s.Shiny = 100f;
        scene.AddShape(ref s);

        Shape p1 = new Plane();
        p1.Shiny = 128f;
        p1.D = new Vector(0, 0, 255);
        scene.AddShape(ref p1);

        Shape s2 = new Sphere(new Vector(4.0f, 1.0f, 0.0f), 1f);
        s2.D = new Vector(200f, 0.0f, 255f);
        scene.AddShape(ref s2);

        Shape s3 = new Sphere(new Vector(5.0f, 0.5f, 1.0f), 0.5f);
        s3.D = new Vector(0.0f, 255f, 0.0f);
        scene.AddShape(ref s3);

        c2.RenderImage("SphereScene2.bmp", scene);

        Vector eye = new Vector(0,3,0);
        Vector lookAt = new Vector();
        Camera cam = new Camera(Camera.Projection.Orthographic,
            eye, lookAt, new Vector(0.0f, 0f, 1f),
            0.1f, 150f, 512, 512, -10f, 10f, -10f, 10f);
        
        Scene sphereArray = new Scene();
        sphereArray.Light = new Vector(0f,15f,10f);
        
        float spacing = 2f;
        var rand = new Random();
        for(int i = -9; i <= 9; i+=2){
            for (int j = -9; j <= 9; j+=2){
                Shape sphere = new Sphere(new Vector(i, 0, j), 1f);
                sphere.D = new Vector(rand.Next(0,255), rand.Next(0,255), rand.Next(0,255));
                sphere.Shiny = (float) (rand.NextDouble() * 127) + 1;
                sphereArray.AddShape(ref sphere);
            }
        }
        cam.RenderImage("SphereArray.bmp", sphereArray);
    }
}


