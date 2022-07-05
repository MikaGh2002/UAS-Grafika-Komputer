using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;


namespace _3D_Object
{
    static class Constants
    {
        public const string path = "../../../Shaders/";
    }
    internal class Window : GameWindow
    {
        float _time;
        Camera _camera;
        Vector3 _mainCharacterPos = new Vector3(0.0f, 0.0f, 0.0f);
        float _rotationSpeed = 1f;
        int counter = 0;
        int habisMati = 0;
        int hadapMana = 0;
        Asset3d LightObject = new Asset3d();
        Asset3d LightObject2 = new Asset3d();
        Asset3d pagar = new Asset3d();
        Asset3d pagar2 = new Asset3d();
        Asset3d pagar3 = new Asset3d();
        Asset3d pagar4 = new Asset3d();
        Asset3d pagar5 = new Asset3d();
        Asset3d pagar6 = new Asset3d();
        Asset3d pagar7 = new Asset3d();
        Asset3d pagar8 = new Asset3d();
        Asset3d pagar9 = new Asset3d();
        Asset3d pagar10 = new Asset3d();
        Asset3d tanganKiri = new Asset3d();
        Asset3d tanganKanan = new Asset3d();
        Asset3d kakiKiri = new Asset3d();
        Asset3d kakiKanan = new Asset3d();
        Asset3d kepala = new Asset3d();
        Asset3d badan = new Asset3d();
        Asset3d latar = new Asset3d();
        Asset3d babi = new Asset3d();
        Asset3d aksesorisBabi = new Asset3d();
        Asset3d anjing = new Asset3d();
        Asset3d aksesorisAnjing = new Asset3d();
        Asset3d panda1 = new Asset3d();
        Asset3d panda2 = new Asset3d();
        Asset3d bear1 = new Asset3d();
        Asset3d bear2 = new Asset3d();
        Asset3d bebek1 = new Asset3d();
        Asset3d bebek2 = new Asset3d();
        Asset3d bebek3 = new Asset3d();
        Asset3d redPanda1 = new Asset3d();
        Asset3d redPanda2 = new Asset3d();
        Asset3d redPanda3 = new Asset3d();
        Asset3d polarBear1 = new Asset3d();
        Asset3d polarBear2 = new Asset3d();
        Asset3d llama1 = new Asset3d();
        Asset3d llama2 = new Asset3d();
        Asset3d llama3 = new Asset3d();
        Asset3d camel1 = new Asset3d();
        Asset3d camel2 = new Asset3d();
        Asset3d camel3 = new Asset3d();
        Asset3d sheep1 = new Asset3d();
        Asset3d sheep2 = new Asset3d();
        Asset3d sheep3 = new Asset3d();
        Asset3d tree1 = new Asset3d();
        Asset3d tree2 = new Asset3d();
        Asset3d tree3 = new Asset3d();
        Asset3d tree4 = new Asset3d();
        Asset3d tree5 = new Asset3d();
        Asset3d tree6 = new Asset3d();
        Asset3d tree7 = new Asset3d();
        Asset3d tree8 = new Asset3d();
        Asset3d tree9 = new Asset3d();
        Asset3d tree10 = new Asset3d();
        Asset3d treeBundar1 = new Asset3d();
        Asset3d treeBundar2 = new Asset3d();
        Asset3d treeBundar3 = new Asset3d();
        Asset3d treeBundar4 = new Asset3d();
        Asset3d treeBundar5 = new Asset3d();
        Asset3d treeBundar6 = new Asset3d();
        Asset3d treeBundar7 = new Asset3d();
        Asset3d treeBundar8 = new Asset3d();
        Asset3d treeBundar9 = new Asset3d();
        Asset3d treeBundar10 = new Asset3d();
        Vector3 spotlightPos = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 putih = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 kuning = new Vector3(1.0f, 1.0f, 0.4f);
        Vector3 biru = new Vector3(0.6f, 1.0f, 1.0f);
        Vector3 pink = new Vector3(0.96f,0.76f,0.76f);
        Vector3 hitam = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 cokelat = new Vector3(0.59f,0.29f,0.0f);
        Vector3 cokelat2 = new Vector3(0.54f, 0.27f, 0.0f);
        Vector3 kuning2 = new Vector3(1.0f, 0.68f, 0.25f);
        Vector3 putih2 = new Vector3(1.0f, 0.8f, 1.0f);
        Vector3 lightRed = new Vector3(0.61f, 0.52f, 0.34f);
        Vector3 brickRed = new Vector3(0.61f, 0.1f, 0f);
        Vector3 darkBrown = new Vector3(0.2f, 0.02f, 0f);
        Vector3 cream = new Vector3(1.0f, 0.6f, 0.44f);
        Vector3 pinkTree = new Vector3(1.0f, 0.71f, 0.77f);
        Vector3 pinkTreeTua = new Vector3(0.83f, 0.36f, 0.38f);
        Vector3 colorLamp;
        List<Asset3d> manusiaKotak = new List<Asset3d>();
        List<Vector3> warnaSenter = new List<Vector3>();
        List<Asset3d> environment = new List<Asset3d>();

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }
        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }
        public void createPagar(float x, float y, float z)
        {
            Asset3d pagar1 = new Asset3d();pagar1.createCustom(x, y + 0.01f, z, 0.08f, 0.01f, 0.01f);
            Asset3d pagar2 = new Asset3d();pagar2.createCustom(x, y - 0.01f, z, 0.08f, 0.01f, 0.01f);
            Asset3d pagar3 = new Asset3d();pagar3.createCustom(x + 0.02f, y, z, 0.01f, 0.06f, 0.01f);
            Asset3d pagar4 = new Asset3d();pagar4.createCustom(x - 0.02f, y, z, 0.01f, 0.06f, 0.01f);
            environment.Add(pagar1); environment.Add(pagar2); environment.Add(pagar3); environment.Add(pagar4);
        }

        public void createPagar2(float x, float y, float z)
        {
            Asset3d pagar1 = new Asset3d();pagar1.createCustom(x, y + 0.01f, z, 0.01f, 0.01f, 0.08f);
            Asset3d pagar2 = new Asset3d();pagar2.createCustom(x, y - 0.01f, z, 0.01f, 0.01f, 0.08f);
            Asset3d pagar3 = new Asset3d();pagar3.createCustom(x, y, z + 0.02f, 0.01f, 0.06f, 0.01f);
            Asset3d pagar4 = new Asset3d();pagar4.createCustom(x, y, z - 0.02f, 0.01f, 0.06f, 0.01f);
            environment.Add(pagar1); environment.Add(pagar2); environment.Add(pagar3); environment.Add(pagar4);
        }

        public void createTreeBundar1(float x, float y, float z)
        {
            Asset3d tree1 = new Asset3d(); tree1.createCustom(x - 1.2f, y + 0.017f, z - 0.1f, 0.05f, 0.2f, 0.05f);
            Asset3d tree2 = new Asset3d(); tree2.createCustom(x - 1.2f, y + 0.12f, z - 0.07f, 0.035f, 0.07f, 0.04f);
            Asset3d tree3 = new Asset3d(); tree3.createCustom(x - 1.2f, y + 0.06f, z - 0.12f, 0.03f, 0.02f, 0.09f);
            Asset3d tree4 = new Asset3d(); tree4.createCustom(x - 1.2f, y + 0.09f, z - 0.155f, 0.025f, 0.04f, 0.02f);
            environment.Add(tree1); environment.Add(tree2); environment.Add(tree3); environment.Add(tree4);
        }
        public void createTreeBundar2(float x, float y, float z)
        {
            Asset3d tree1 = new Asset3d(); tree1.createCustom(x - 1.2f, y + 0.17f, z - 0.07f, 0.15f, 0.04f, 0.15f);
            Asset3d tree2 = new Asset3d(); tree2.createCustom(x - 1.2f, y + 0.2f, z - 0.07f, 0.16f, 0.05f, 0.16f);
            Asset3d tree3 = new Asset3d(); tree3.createCustom(x - 1.2f, y + 0.23f, z - 0.07f, 0.15f, 0.05f, 0.15f);
            Asset3d tree4 = new Asset3d(); tree4.createCustom(x - 1.2f, y + 0.26f, z - 0.07f, 0.15f, 0.05f, 0.1f);
            Asset3d tree5 = new Asset3d(); tree5.createCustom(x - 1.2f, y + 0.1f, z - 0.16f, 0.05f, 0.02f, 0.065f);
            Asset3d tree6 = new Asset3d(); tree6.createCustom(x - 1.2f, y + 0.12f, z - 0.16f, 0.045f, 0.02f, 0.05f);
            environment.Add(tree1); environment.Add(tree2); environment.Add(tree3); environment.Add(tree4); environment.Add(tree5); environment.Add(tree6);
        }

        public void createKandang(float x, float y, float z)
        {
            createPagar(x, y, z);
            createPagar(x + 0.08f, y, z);
            createPagar(x - 0.08f, y, z);
            createPagar(x, y, z - 0.24f);
            createPagar(x + 0.08f, y, z - 0.24f);
            createPagar(x - 0.08f, y, z - 0.24f);
            createPagar2(x + 0.12f, y, z - 0.04f);
            createPagar2(x + 0.12f, y, z - 0.12f);
            createPagar2(x + 0.12f, y, z - 0.20f);
            createPagar2(x - 0.12f, y, z - 0.04f);
            createPagar2(x - 0.12f, y, z - 0.12f);
            createPagar2(x - 0.12f, y, z - 0.20f);
        }

        public void createTree1(float x, float y, float z)
        {
            Asset3d tree1 = new Asset3d();tree1.createCustom(x - 0.9f, y + 0.017f, z - 0.1f, 0.05f, 0.2f, 0.05f);
            Asset3d tree2 = new Asset3d(); tree2.createCustom(x - 0.9f, y + 0.12f, z - 0.07f, 0.035f, 0.07f, 0.04f);
            Asset3d tree3 = new Asset3d(); tree3.createCustom(x - 0.9f, y + 0.06f, z - 0.12f, 0.03f, 0.02f, 0.09f);
            Asset3d tree4 = new Asset3d(); tree4.createCustom(x - 0.9f, y + 0.09f, z - 0.155f, 0.025f, 0.04f, 0.02f);
            environment.Add(tree1); environment.Add(tree2); environment.Add(tree3); environment.Add(tree4);
        }
        public void createTree2(float x, float y, float z)
        {
            Asset3d tree1 = new Asset3d(); tree1.createCustom(x - 0.9f, y + 0.17f, z - 0.07f, 0.2f, 0.04f, 0.2f);
            Asset3d tree2 = new Asset3d(); tree2.createCustom(x - 0.9f, y + 0.2f, z - 0.07f, 0.15f, 0.05f, 0.15f);
            Asset3d tree3 = new Asset3d(); tree3.createCustom(x - 0.9f, y + 0.23f, z - 0.07f, 0.1f, 0.05f, 0.1f);
            Asset3d tree4 = new Asset3d(); tree4.createCustom(x - 0.9f, y + 0.26f, z - 0.07f, 0.05f, 0.05f, 0.05f);
            Asset3d tree5 = new Asset3d(); tree5.createCustom(x - 0.9f, y + 0.1f, z - 0.16f, 0.05f, 0.02f, 0.065f);
            Asset3d tree6 = new Asset3d(); tree6.createCustom(x - 0.9f, y + 0.12f, z - 0.16f, 0.045f, 0.02f, 0.05f);
            environment.Add(tree1); environment.Add(tree2); environment.Add(tree3); environment.Add(tree4); environment.Add(tree5); environment.Add(tree6);
        }
        public void loadKandang(Asset3d obj,float x, float y, float z)
        {
            createKandang(x, y, z);
            obj.createKandang(x,y,z);
            obj.load_withnormal(Constants.path + "shaderwithNormal.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
        }
        public void loadObject(Asset3d obj, float x, float y, float z, float panjangX, float panjangY, float panjangZ)
        {
            obj.createCustom(x,y,z,panjangX,panjangY,panjangZ);
            obj.load_withnormal(Constants.path + "shaderwithNormal.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
        }
        public void loadShader(Asset3d obj)
        {
            obj.load_withnormal(Constants.path + "shaderwithNormal.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            //ganti background
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            warnaSenter.Add(putih);
            warnaSenter.Add(kuning);
            warnaSenter.Add(biru);
            colorLamp = biru;
            LightObject.createBoxVertices(0.0f, 1.0f, 0.0f, 0.05f);
            LightObject.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            LightObject2.createBoxVertices(0.0f, 0.09f, -0.09f, 0.005f);
            LightObject2.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            loadKandang(pagar, 0.0f, 0.0f, 0.0f);
            loadObject(badan, 0.0f, 0.03f, 0.15f, 0.03f, 0.05f, 0.015f);
            loadObject(kepala, 0.0f, 0.07f, 0.15f, 0.028f, 0.028f, 0.014f);
            loadObject(tanganKanan, 0.019f, 0.035f, 0.15f,0.01f, 0.037f, 0.01f);
            loadObject(tanganKiri, -0.019f, 0.035f, 0.15f, 0.01f, 0.037f, 0.01f);
            loadObject(kakiKanan, 0.009f, -0.01f, 0.15f, 0.012f, 0.04f, 0.01f);
            loadObject(kakiKiri, -0.009f, -0.01f, 0.15f, 0.012f, 0.04f, 0.01f);
            loadObject(latar, -0.009f, -0.04f, -0.05f, 4.0f, 0.01f, 1.5f);
            babi.createBabi(-0.6f, 0.0f, -0.06f);
            loadShader(babi);
            aksesorisBabi.createAksesorisBabi(-0.6f, 0.0f, -0.06f);
            loadShader(aksesorisBabi);
            loadKandang(pagar2,0.3f,0.0f,0.0f);
            loadKandang(pagar3, -0.3f, 0.0f, 0.0f);
            loadKandang(pagar4, 0.6f, 0.0f, 0.0f);
            loadKandang(pagar5, -0.6f, 0.0f, 0.0f);
            loadKandang(pagar6, 0.9f, 0.0f, 0.0f);
            loadKandang(pagar7, 1.2f, 0.0f, 0.0f);
            loadKandang(pagar8, 1.5f, 0.0f, 0.0f);
            loadKandang(pagar9, 1.8f, 0.0f, 0.0f);
            loadKandang(pagar10, 2.1f, 0.0f, 0.0f);
            anjing.createAnjing(-0.3f, 0.0f, -0.06f);
            loadShader(anjing);
            aksesorisAnjing.createAksesorisAnjing(-0.3f, 0.0f, -0.06f);
            loadShader(aksesorisAnjing);
            panda1.createPanda1(0.0f, 0.0f, 0.0f);
            loadShader(panda1);
            panda2.createPanda2(0.0f, 0.0f, 0.0f);
            loadShader(panda2);
            bear1.createBear1(0.0f, 0.0f, 0.0f);
            loadShader(bear1);
            bear2.createBear2(0.0f, 0.0f, 0.0f);
            loadShader(bear2);
            bebek1.createBebek1(0.0f, 0.0f, 0.0f);
            loadShader(bebek1);
            bebek2.createBebek2(0.0f, 0.0f, 0.0f);
            loadShader(bebek2);
            bebek3.createBebek3(0.0f, 0.0f, 0.0f);
            loadShader(bebek3);
            redPanda1.createRedPanda1(0.0f, 0.0f, 0.0f);
            loadShader(redPanda1);
            redPanda2.createRedPanda2(0.0f, 0.0f, 0.0f);
            loadShader(redPanda2);
            redPanda3.createRedPanda3(0.0f, 0.0f, 0.0f);
            loadShader(redPanda3);
            polarBear1.createPolarBear1(0.0f, 0.0f, 0.0f);
            loadShader(polarBear1);
            polarBear2.createPolarBear2(0.0f, 0.0f, 0.0f);
            loadShader(polarBear2);
            llama1.createLlama1(0.0f, 0.0f, 0.0f);
            loadShader(llama1);
            llama2.createLlama2(0.0f, 0.0f, 0.0f);
            loadShader(llama2);
            llama3.createLlama3(0.0f, 0.0f, 0.0f);
            loadShader(llama3);
            camel1.createCamel1(0.0f, 0.0f, 0.0f);
            loadShader(camel1);
            camel2.createCamel2(0.0f, 0.0f, 0.0f);
            loadShader(camel2);
            camel3.createCamel3(0.0f, 0.0f, 0.0f);
            loadShader(camel3);
            sheep1.createSheep1(0.0f, 0.0f, 0.0f);
            loadShader(sheep1);
            sheep2.createSheep2(0.0f, 0.0f, 0.0f);
            loadShader(sheep2);
            sheep3.createSheep3(0.0f, 0.0f, 0.0f);
            loadShader(sheep3);
            tree1.createTree1(0.0f, 0.0f, 0.5f);
            loadShader(tree1);
            createTree1(0.0f, 0.0f, 0.5f);
            tree2.createTree2(0.0f, 0.0f, 0.5f);
            loadShader(tree2);
            createTree2(0.0f, 0.0f, 0.5f);
            tree3.createTree1(0.3f, 0.0f, 0.5f);
            loadShader(tree3);
            createTree1(0.3f, 0.0f, 0.5f);
            tree4.createTree2(0.3f, 0.0f, 0.5f);
            loadShader(tree4);
            createTree2(0.3f, 0.0f, 0.5f);
            tree5.createTree1(0.9f, 0.0f, 0.5f);
            loadShader(tree5);
            createTree1(0.9f, 0.0f, 0.5f);
            tree6.createTree2(0.9f, 0.0f, 0.5f);
            loadShader(tree6);
            createTree2(0.9f, 0.0f, 0.5f);
            tree7.createTree1(1.5f, 0.0f, 0.5f);
            loadShader(tree7);
            createTree1(1.5f, 0.0f, 0.5f);
            tree8.createTree2(1.5f, 0.0f, 0.5f);
            loadShader(tree8);
            createTree2(1.5f, 0.0f, 0.5f);
            tree9.createTree1(2.1f, 0.0f, 0.5f);
            loadShader(tree9);
            createTree1(2.1f, 0.0f, 0.5f);
            tree10.createTree2(2.1f, 0.0f, 0.5f);
            loadShader(tree10);
            createTree2(2.1f, 0.0f, 0.5f);
            treeBundar1.createTreeBundar1(0.0f, 0.0f, 0.5f);
            loadShader(treeBundar1);
            createTreeBundar1(0.0f, 0.0f, 0.5f);
            treeBundar2.createTreeBundar2(0.0f, 0.0f, 0.5f);
            loadShader(treeBundar2);
            createTreeBundar2(0.0f, 0.0f, 0.5f);
            treeBundar3.createTreeBundar1(0.9f, 0.0f, 0.5f);
            loadShader(treeBundar3);
            createTreeBundar1(0.9f, 0.0f, 0.5f);
            treeBundar4.createTreeBundar2(0.9f, 0.0f, 0.5f);
            loadShader(treeBundar4);
            createTreeBundar2(0.9f, 0.0f, 0.5f);
            treeBundar5.createTreeBundar1(1.5f, 0.0f, 0.5f);
            loadShader(treeBundar5);
            createTreeBundar1(1.5f, 0.0f, 0.5f);
            treeBundar6.createTreeBundar2(1.5f, 0.0f, 0.5f);
            loadShader(treeBundar6);
            createTreeBundar2(1.5f, 0.0f, 0.5f);
            treeBundar7.createTreeBundar1(2.1f, 0.0f, 0.5f);
            loadShader(treeBundar7);
            createTreeBundar1(2.1f, 0.0f, 0.5f);
            treeBundar8.createTreeBundar2(2.1f, 0.0f, 0.5f);
            loadShader(treeBundar8);
            createTreeBundar2(2.1f, 0.0f, 0.5f);
            treeBundar9.createTreeBundar1(2.7f, 0.0f, 0.5f);
            loadShader(treeBundar9);
            createTreeBundar1(2.7f, 0.0f, 0.5f);
            treeBundar10.createTreeBundar2(2.7f, 0.0f, 0.5f);
            loadShader(treeBundar10);
            createTreeBundar2(2.7f, 0.0f, 0.5f);
            manusiaKotak.Add(kepala);
            manusiaKotak.Add(badan);
            manusiaKotak.Add(tanganKanan);
            manusiaKotak.Add(tanganKiri);
            manusiaKotak.Add(kakiKiri);
            manusiaKotak.Add(kakiKanan);

            _camera = new Camera(new Vector3(0.0f, 0.2f, 0.2f), Size.X / Size.Y);
            _camera.Pitch = -45f;
            _mainCharacterPos = manusiaKotak[1]._centerPosition;
            CursorGrabbed = false;
        }

        public void renders(Asset3d obj, Matrix4 temp, Vector3 color)
        {
            obj.render(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            obj.setFragVariable(color, colorLamp);
            obj.setSpecPower(0.2f);
            obj.setAmbiPower(0.2f);
            obj.setDiffPower(new Vector3(0.3f, 0.3f, 0.3f));
            float temps = _camera.Pitch;
            _camera.Pitch = 0.0f;
            _camera.Pitch = temps;
            obj.setSpot(spotlightPos, 30f);
            obj.setSpecularDiffuseVariable(LightObject._centerPosition, LightObject2._centerPosition, _camera.Position);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            //
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //_time += 9.0 * args.Time;
            Matrix4 temp = Matrix4.Identity;
            //temp = temp * Matrix4.CreateTranslation(0.5f, 0.5f, 0.0f);
            //degr += MathHelper.DegreesToRadians(20f);
            //temp = temp * Matrix4.CreateRotationX(degr);
            //_object3d[0].rotatede(_object3d[0]._centerPosition, _object3d[0]._euler[1], 0);
            //_object3d[0].Child[0].rotatede(_object3d[0].Child[0]._centerPosition, _object3d[0].Child[0]._euler[0], 1);
            renders(pagar,temp, pink);
            renders(badan,temp, pink);
            renders(kepala, temp, pink);
            renders(tanganKanan, temp, pink);
            renders(tanganKiri, temp, pink);
            renders(kakiKanan, temp, pink);
            renders(kakiKiri, temp, pink);
            renders(latar, temp, cokelat);
            renders(babi, temp, pink);
            renders(aksesorisBabi, temp, hitam);
            renders(pagar2, temp, biru);
            renders(pagar3, temp, biru);
            renders(pagar4, temp, biru);
            renders(pagar5, temp, biru);
            renders(pagar6, temp, biru);
            renders(pagar7, temp, biru);
            renders(pagar8, temp, biru);
            renders(pagar9, temp, biru);
            renders(pagar10, temp, biru);
            renders(anjing, temp, pink);
            renders(aksesorisAnjing,temp,hitam);
            renders(panda1, temp,putih2);
            renders(panda2, temp, hitam);
            renders(bear1, temp, cokelat2);
            renders(bear2, temp, hitam);
            renders(bebek1, temp, putih2);
            renders(bebek2, temp, kuning2);
            renders(bebek3, temp, hitam);
            renders(redPanda1, temp, brickRed);
            renders(redPanda2, temp, lightRed);
            renders(redPanda3, temp, darkBrown);
            renders(polarBear1, temp, putih2);
            renders(polarBear2, temp, hitam);
            renders(llama1, temp, putih2);
            renders(llama2, temp, cokelat2);
            renders(llama3, temp, cream);
            renders(camel1, temp, cokelat2);
            renders(camel2, temp, hitam);
            renders(camel3, temp, darkBrown);
            renders(sheep1, temp, putih2);
            renders(sheep2, temp, cream);
            renders(sheep3, temp, darkBrown);
            renders(tree1, temp, darkBrown);
            renders(tree2, temp, pinkTree);
            renders(tree3, temp, darkBrown);
            renders(tree4, temp, pinkTree);
            renders(tree5, temp, darkBrown);
            renders(tree6, temp, pinkTree);
            renders(tree7, temp, darkBrown);
            renders(tree8, temp, pinkTree);
            renders(tree9, temp, darkBrown);
            renders(tree10, temp, pinkTree);
            renders(treeBundar1, temp, darkBrown);
            renders(treeBundar2, temp, pinkTreeTua);
            renders(treeBundar3, temp, darkBrown);
            renders(treeBundar4, temp, pinkTreeTua);
            renders(treeBundar5, temp, darkBrown);
            renders(treeBundar6, temp, pinkTreeTua);
            renders(treeBundar7, temp, darkBrown);
            renders(treeBundar8, temp, pinkTreeTua);
            renders(treeBundar9, temp, darkBrown);
            renders(treeBundar10, temp, pinkTreeTua);
            LightObject.render(3, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            LightObject.setFragVariable2(new Vector3(1.0f,1.0f,1.0f));
            LightObject2.render(3, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            LightObject2.setFragVariable2(colorLamp);

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }

        public bool CheckElapse(List<Asset3d> target, List<Asset3d> character, float x, float y, float z)
        {
            bool notElapse = true;
            foreach (Asset3d environtment in target)
            {
                bool elapseX = false;
                bool elapseY = false;
                bool elapseZ = false;
                Vector3 enviBoundMin = environtment.getMinCoord();
                Vector3 enviBoundMax = environtment.getMaxCoord();
                foreach (Asset3d chars in character)
                {
                    Vector3 enviBoundMins = enviBoundMin - chars.getRad();
                    Vector3 enviBoundMaxs = enviBoundMax + chars.getRad();
                    Vector3 charTrans = chars.getTranslateResult(x, y, z);
                    if(charTrans.X >= enviBoundMins.X && charTrans.X <= enviBoundMaxs.X){
                        elapseX = true;
                    }
                    if (charTrans.Y >= enviBoundMins.Y && charTrans.Y <= enviBoundMaxs.Y)
                    {
                        elapseY = true;
                    }
                    if (charTrans.Z >= enviBoundMins.Z && charTrans.Z <= enviBoundMaxs.Z)
                    {
                        elapseZ = true;
                    }
                    if (elapseX == true && elapseY == true && elapseZ == true)
                    {
                        notElapse = false;
                        break;
                    }
                }
                if(notElapse == false)
                {
                    break;
                }
            }
            return notElapse;
        }
        public bool CheckElapse2(List<Asset3d> target, List<Asset3d> character, float x, float y, float z)
        {
            bool notElapse = true;
            foreach (Asset3d environtment in target)
            {
                bool elapseX = false;
                bool elapseY = false;
                bool elapseZ = false;
                Vector3 enviBoundMin = environtment.getMinCoord();
                Vector3 enviBoundMax = environtment.getMaxCoord();
                foreach (Asset3d chars in character)
                {
                    Vector3 charRad = new Vector3(chars.getRad().Z, chars.getRad().Y, chars.getRad().X);
                    Vector3 enviBoundMins = enviBoundMin - charRad;
                    //Menyesuaikan Ukuran Elapse
                    enviBoundMins.Z = enviBoundMin.Z - (2 * charRad.Z);
                    enviBoundMins.X = enviBoundMin.X - (2 * charRad.X);
                    Vector3 enviBoundMaxs = enviBoundMax + charRad;
                    //Menyesuaikan Ukuran Elapse
                    enviBoundMaxs.Z = enviBoundMax.Z + (2 * charRad.Z);
                    enviBoundMaxs.X = enviBoundMax.X - (2 * charRad.X);
                    Vector3 charTrans = chars.getTranslateResult(x, y, z);
                    if (charTrans.X >= enviBoundMins.X && charTrans.X <= enviBoundMaxs.X)
                    {
                        elapseX = true;
                    }
                    if (charTrans.Y >= enviBoundMins.Y && charTrans.Y <= enviBoundMaxs.Y)
                    {
                        elapseY = true;
                    }
                    if (charTrans.Z >= enviBoundMins.Z && charTrans.Z <= enviBoundMaxs.Z)
                    {
                        elapseZ = true;
                    }
                    if (elapseX == true && elapseY == true && elapseZ == true)
                    {
                        notElapse = false;
                        break;
                    }
                }
                if (notElapse == false)
                {
                    break;
                }
            }
            return notElapse;
        }


        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyPressed(Keys.RightShift))
            {
                colorLamp = new Vector3(0.0f, 0.0f, 0.0f);
                habisMati = 1;
            }
            if (KeyboardState.IsKeyReleased(Keys.D1))
            {
                float muterBerapa = 360.0f - _camera.Yaw - 90.0f;
                foreach (Asset3d kotak in manusiaKotak)
                {
                    kotak.rotate(new Vector3(0.00f, 0.03f, 0.15f), new Vector3(0.0f, -1.0f, 0.0f), muterBerapa);
                }

                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _mainCharacterPos;
                _camera.Yaw += muterBerapa;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _mainCharacterPos, muterBerapa).ExtractRotation());
                _camera.Position += _mainCharacterPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _mainCharacterPos);

                hadapMana = 0;
            }
            if (KeyboardState.IsKeyReleased(Keys.D2))
            {
                float muterBerapa = 360.0f - _camera.Yaw;
                foreach (Asset3d kotak in manusiaKotak)
                {
                    kotak.rotate(new Vector3(0.00f, 0.03f, 0.15f), new Vector3(0.0f, -1.0f, 0.0f), muterBerapa);
                }
                //float pitchTemp = _camera.Pitch;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _mainCharacterPos;
                _camera.Yaw += muterBerapa;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _mainCharacterPos, muterBerapa).ExtractRotation());
                _camera._front = -Vector3.Normalize(_camera.Position - _mainCharacterPos);
                _camera.Position += _mainCharacterPos;
                hadapMana = 1;
            }
            if (KeyboardState.IsKeyReleased(Keys.D3))
            {
                float muterBerapa = 360.0f - _camera.Yaw - 270.0f;
                foreach (Asset3d kotak in manusiaKotak)
                {
                    kotak.rotate(new Vector3(0.00f, 0.03f, 0.15f), new Vector3(0.0f, -1.0f, 0.0f), muterBerapa);
                }
                float pitchTemp = _camera.Pitch;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _mainCharacterPos;
                _camera.Yaw += muterBerapa;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _mainCharacterPos, muterBerapa).ExtractRotation());
                _camera.Position += _mainCharacterPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _mainCharacterPos);
                _camera.Pitch = pitchTemp;
                hadapMana = 2;
            }
            if (KeyboardState.IsKeyReleased(Keys.D4))
            {
                float muterBerapa = 360.0f - _camera.Yaw - 180.0f;
                foreach (Asset3d kotak in manusiaKotak)
                {
                    kotak.rotate(new Vector3(0.00f, 0.03f, 0.15f), new Vector3(0.0f, -1.0f, 0.0f), muterBerapa);
                }
                float pitchTemp = _camera.Pitch;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _mainCharacterPos;
                _camera.Yaw += muterBerapa;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _mainCharacterPos, muterBerapa).ExtractRotation());
                _camera.Position += _mainCharacterPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _mainCharacterPos);
                _camera.Pitch = pitchTemp;
                hadapMana = 3;
            }
            if (KeyboardState.IsKeyPressed(Keys.T))
            {
                LightObject2.translate(0.0f, 0.0f, -0.01f);
            }
            if (KeyboardState.IsKeyPressed(Keys.G))
            {
                LightObject2.translate(0.0f, 0.0f, 0.01f);
            }
            if (KeyboardState.IsKeyPressed(Keys.F))
            {
                LightObject2.translate(-0.01f, 0.0f, 0.0f);
            }
            if (KeyboardState.IsKeyPressed(Keys.H))
            {
                LightObject2.translate(0.01f, 0.0f, 0.0f);
            }
            if (KeyboardState.IsKeyPressed(Keys.R))
            {
                LightObject2.translate(0.0f, 0.01f, 0.0f);
            }
            if (KeyboardState.IsKeyPressed(Keys.Y))
            {
                LightObject2.translate(0.0f, -0.01f, 0.0f);
            }
            if (KeyboardState.IsKeyPressed(Keys.L))
            {
                if (counter == 2)
                {
                    counter = 0;
                }
                else if (counter != 0 && habisMati == 1)
                {
                    counter = 0;
                    habisMati = 0;
                }
                else
                {
                    counter += 1;
                }
                colorLamp = warnaSenter[counter];
            }
            float cameraSpeed = 0.001f;
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                bool notElapse = true;
                if (hadapMana == 0)
                {
                    notElapse = CheckElapse(environment, manusiaKotak, 0.0f, 0.0f, -0.001f);
                }
                if (hadapMana == 1)
                {
                    notElapse = CheckElapse2(environment, manusiaKotak, 0.001f, 0.0f, 0.0f);
                }
                if (hadapMana == 2)
                {
                    notElapse = CheckElapse(environment, manusiaKotak, 0.0f, 0.0f, 0.001f);
                }
                if (hadapMana == 3)
                {
                    notElapse = CheckElapse2(environment, manusiaKotak, -0.001f, 0.0f, 0.0f);
                }
                if (notElapse) {
                    if (hadapMana == 0)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position -= new Vector3(0.0f, 0.0f, cameraSpeed);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.0f, 0.0f, -0.001f);
                        }
                        _mainCharacterPos += new Vector3(0.0f, 0.0f, -0.001f);
                    }
                    if (hadapMana == 1)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position += new Vector3(cameraSpeed, 0.0f, 0.0f);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.001f, 0.0f, 0.0f);
                        }
                        _mainCharacterPos += new Vector3(0.001f, 0.0f, 0.0f);
                    }
                    if (hadapMana == 2)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position += new Vector3(0.0f, 0.0f, cameraSpeed);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.0f, 0.0f, 0.001f);
                        }
                        _mainCharacterPos += new Vector3(0.0f, 0.0f, 0.001f);
                    }
                    if (hadapMana == 3)
                    {
                        _camera.Position -= new Vector3(cameraSpeed, 0.0f, 0.0f);
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(-0.001f, 0.0f, 0.0f);
                        }
                        _mainCharacterPos += new Vector3(-0.001f, 0.0f, 0.0f);
                    }
                }
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                bool notElapse = true;
                if (hadapMana == 0)
                {
                    notElapse = CheckElapse(environment, manusiaKotak, 0.0f, 0.0f, 0.001f);
                }
                if (hadapMana == 1)
                {
                    notElapse = CheckElapse2(environment, manusiaKotak, -0.001f, 0.0f, 0.0f);
                }
                if (hadapMana == 2)
                {
                    notElapse = CheckElapse(environment, manusiaKotak, 0.0f, 0.0f, -0.001f);
                }
                if (hadapMana == 3)
                {
                    notElapse = CheckElapse2(environment, manusiaKotak, 0.001f, 0.0f, 0.0f);
                }
                if (notElapse)
                {
                    if (hadapMana == 0)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position += new Vector3(0.0f, 0.0f, cameraSpeed);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.0f, 0.0f, 0.001f);
                        }
                        _mainCharacterPos += new Vector3(0.0f, 0.0f, 0.001f);
                    }
                    if (hadapMana == 1)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position -= new Vector3(cameraSpeed, 0.0f, 0.0f);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(-0.001f, 0.0f, 0.0f);
                        }
                        _mainCharacterPos += new Vector3(-0.001f, 0.0f, 0.0f);
                    }
                    if (hadapMana == 2)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position -= new Vector3(0.0f, 0.0f, cameraSpeed);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.0f, 0.0f, -0.001f);
                        }
                        _mainCharacterPos += new Vector3(0.0f, 0.0f, -0.001f);
                    }
                    if (hadapMana == 3)
                    {
                        _camera.Position += new Vector3(cameraSpeed, 0.0f, 0.0f);
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.001f, 0.0f, 0.0f);
                        }
                        _mainCharacterPos += new Vector3(0.001f, 0.0f, 0.0f);
                    }
                }
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                bool notElapse = true;
                if (hadapMana == 0)
                {
                    notElapse = CheckElapse(environment, manusiaKotak, -0.001f, 0.0f, 0.0f);
                }
                if (hadapMana == 1)
                {
                    notElapse = CheckElapse2(environment, manusiaKotak, 0.0f, 0.0f, -0.001f);
                }
                if (hadapMana == 2)
                {
                    notElapse = CheckElapse(environment, manusiaKotak, 0.001f, 0.0f, 0.0f);
                }
                if (hadapMana == 3)
                {
                    notElapse = CheckElapse2(environment, manusiaKotak, 0.0f, 0.0f, 0.001f);
                }
                if (notElapse)
                {
                    if (hadapMana == 1)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position -= new Vector3(0.0f, 0.0f, cameraSpeed);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.0f, 0.0f, -0.001f);
                        }
                        _mainCharacterPos += new Vector3(0.0f, 0.0f, -0.001f);
                    }
                    if (hadapMana == 2)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position += new Vector3(cameraSpeed, 0.0f, 0.0f);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.001f, 0.0f, 0.0f);
                        }
                        _mainCharacterPos += new Vector3(0.001f, 0.0f, 0.0f);
                    }
                    if (hadapMana == 3)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position += new Vector3(0.0f, 0.0f, cameraSpeed);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.0f, 0.0f, 0.001f);
                        }
                        _mainCharacterPos += new Vector3(0.0f, 0.0f, 0.001f);
                    }
                    if (hadapMana == 0)
                    {
                        _camera.Position -= new Vector3(cameraSpeed, 0.0f, 0.0f);
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(-0.001f, 0.0f, 0.0f);
                        }
                        _mainCharacterPos += new Vector3(-0.001f, 0.0f, 0.0f);
                    }
                }
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                bool notElapse = true;
                if (hadapMana == 0)
                {
                    notElapse = CheckElapse(environment, manusiaKotak, 0.001f, 0.0f, 0.0f);
                }
                if (hadapMana == 1)
                {
                    notElapse = CheckElapse2(environment, manusiaKotak, 0.0f, 0.0f, 0.001f);
                }
                if (hadapMana == 2)
                {
                    notElapse = CheckElapse(environment, manusiaKotak, -0.001f, 0.0f, 0.0f);
                }
                if (hadapMana == 3)
                {
                    notElapse = CheckElapse2(environment, manusiaKotak, 0.0f, 0.0f, -0.001f);
                }
                if (notElapse == true)
                {
                    if (hadapMana == 1)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position += new Vector3(0.0f, 0.0f, cameraSpeed);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.0f, 0.0f, 0.001f);
                        }
                        _mainCharacterPos += new Vector3(0.0f, 0.0f, 0.001f);
                    }
                    if (hadapMana == 2)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position -= new Vector3(cameraSpeed, 0.0f, 0.0f);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(-0.001f, 0.0f, 0.0f);
                        }
                        _mainCharacterPos += new Vector3(-0.001f, 0.0f, 0.0f);
                    }
                    if (hadapMana == 3)
                    {
                        float batas = _camera.Pitch;
                        _camera.Pitch = 0.0f;
                        _camera.Position -= new Vector3(0.0f, 0.0f, cameraSpeed);
                        _camera.Pitch = batas;
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.0f, 0.0f, -0.001f);
                        }
                        _mainCharacterPos += new Vector3(0.0f, 0.0f, -0.001f);
                    }
                    if (hadapMana == 0)
                    {
                        _camera.Position += new Vector3(cameraSpeed, 0.0f, 0.0f);
                        foreach (Asset3d bagian in manusiaKotak)
                        {
                            bagian.translate(0.001f, 0.0f, 0.0f);
                        }
                        _mainCharacterPos += new Vector3(0.001f, 0.0f, 0.0f);
                    }
                }
            }
            if (KeyboardState.IsKeyDown(Keys.Z))
            {
                foreach (Asset3d bagian in manusiaKotak)
                {
                    bagian.rotate(new Vector3(0.00f,0.03f,0.15f), new Vector3(0.0f, -1.0f, 0.0f), 0.5f);
                }
                float pitchTemp = _camera.Pitch;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _mainCharacterPos;
                _camera.Yaw += 0.5f;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _mainCharacterPos, 0.5f).ExtractRotation());
                _camera.Position += _mainCharacterPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _mainCharacterPos);
                _camera.Pitch = pitchTemp;
            }
            if (KeyboardState.IsKeyDown(Keys.X))
            {
                foreach (Asset3d bagian in manusiaKotak)
                {
                    bagian.rotate(new Vector3(0.00f, 0.03f, 0.15f), new Vector3(0.0f, 1.0f, 0.0f), 0.5f);
                }
                float pitchTemp = _camera.Pitch;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _mainCharacterPos;
                _camera.Yaw -= 0.5f;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _mainCharacterPos, -0.5f)
                    .ExtractRotation());
                _camera.Position += _mainCharacterPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _mainCharacterPos);
                _camera.Pitch = pitchTemp;
            }
            float batasCam = latar.getMaxCoord().Y + 0.02f;
            float transC = 0;
            if (KeyboardState.IsKeyDown(Keys.C))
            {
                float oldY = _camera.Position.Y;
                if (_camera.Position.Y - transC > batasCam)
                {
                    Vector3 axis = new Vector3(0, 0, 0);
                    if (hadapMana == 0)
                    {
                        axis = new Vector3(1, 0, 0);
                    }
                    if (hadapMana == 1)
                    {
                        axis = new Vector3(0, 0, 1);
                    }
                    if (hadapMana == 2)
                    {
                        axis = new Vector3(-1, 0, 0);
                    }
                    if (hadapMana == 3)
                    {
                        axis = new Vector3(0, 0, -1);
                    }
                    _camera.Position -= _mainCharacterPos;
                    _camera.Pitch -= _rotationSpeed;
                    _camera.Position = Vector3.Transform(_camera.Position,
                        generateArbRotationMatrix(axis, _mainCharacterPos, _rotationSpeed).ExtractRotation());
                    _camera.Position += _mainCharacterPos;
                    transC = oldY - _camera.Position.Y;
                }
            }
            float transV = 0;
            if (KeyboardState.IsKeyDown(Keys.V))
            {
                float oldY = _camera.Position.Y;
                if (_camera.Position.Y - transV > batasCam)
                {
                    Vector3 axis = new Vector3(0,0,0);
                    if (hadapMana == 0)
                    {
                        axis = new Vector3(1, 0, 0);
                    }
                    if (hadapMana == 1)
                    {
                        axis = new Vector3(0, 0, 1);
                    }
                    if (hadapMana == 2)
                    {
                        axis = new Vector3(-1, 0, 0);
                    }
                    if (hadapMana == 3)
                    {
                        axis = new Vector3(0, 0, -1);
                    }
                    _camera.Position -= _mainCharacterPos;
                    _camera.Pitch += _rotationSpeed;
                    _camera.Position = Vector3.Transform(_camera.Position,
                        generateArbRotationMatrix(axis, _mainCharacterPos, -_rotationSpeed).ExtractRotation());
                    _camera.Position += _mainCharacterPos;
                    transV = oldY - _camera.Position.Y;
                }
                else
                {
                    Vector3 axis = new Vector3(0, 0, 0);
                    if (hadapMana == 0)
                    {
                        axis = new Vector3(1, 0, 0);
                    }
                    if (hadapMana == 1)
                    {
                        axis = new Vector3(0, 0, 1);
                    }
                    if (hadapMana == 2)
                    {
                        axis = new Vector3(-1, 0, 0);
                    }
                    if (hadapMana == 3)
                    {
                        axis = new Vector3(0, 0, -1);
                    }
                    _camera.Position -= _mainCharacterPos;
                    _camera.Pitch -= _rotationSpeed;
                    _camera.Position = Vector3.Transform(_camera.Position,
                        generateArbRotationMatrix(axis, _mainCharacterPos, _rotationSpeed).ExtractRotation());
                    _camera.Position += _mainCharacterPos;
                }
            }
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                float temp = _camera.Pitch;
                _camera.Pitch = 0.0f;
                _camera.Position += _camera.Up * 0.005f;
                _camera.Pitch = temp;   
            }
            if (KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                float transCam = _camera.Position.Y - 0.005f;
                if (transCam > batasCam)
                {
                    float temp = _camera.Pitch;
                    _camera.Pitch = 0.0f;
                    _camera.Position -= _camera.Up * 0.005f;
                    _camera.Pitch = temp;
                }
            }
        }
    }
}