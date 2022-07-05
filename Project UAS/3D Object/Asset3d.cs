using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3D_Object
{
    internal class Asset3d
    {
        List<Vector3> _vertices = new List<Vector3>();
        List<uint> _indices = new List<uint>();
        int _vertexBufferObject;
        int _vertexArrayObject;
        int _elementBufferObject;
        Shader _shader;
        Matrix4 _view;
        Matrix4 _projection;
        Matrix4 _model;
        public Vector3 _centerPosition;
        public List<Vector3> _euler;
        public List<Asset3d> Child;
        Vector3 minCoord;
        Vector3 maxCoord;
        Vector3 rad;

        public Asset3d(List<Vector3> vertices, List<uint> indices)
        {
            _vertices = vertices;
            _indices = indices;
            setdefault();
        }
        public Asset3d()
        {
            _vertices = new List<Vector3>();
            setdefault();
        }
        public void setdefault()
        {
            _euler = new List<Vector3>();
            //sumbu X
            _euler.Add(new Vector3(1, 0, 0));
            //sumbu y
            _euler.Add(new Vector3(0, 1, 0));
            //sumbu z
            _euler.Add(new Vector3(0, 0, 1));
            _model = Matrix4.Identity;
            _centerPosition = new Vector3(0, 0, 0);
            Child = new List<Asset3d>();

        }
        public void load(string shadervert, string shaderfrag, float Size_x, float Size_y)
        {
            //Buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count
                * Vector3.SizeInBytes, _vertices.ToArray(), BufferUsageHint.StaticDraw);
            //VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            //kalau mau bikin object settingannya beda dikasih if
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float,
                false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            //ada data yang disimpan di _indices
            if (_indices.Count != 0)
            {
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count
                    * sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);
            }
            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();

            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size_x / (float)Size_y, 0.1f, 100.0f);
            foreach (var item in Child)
            {
                item.load(shadervert, shaderfrag, Size_x, Size_y);
            }
        }

        public void render(int _lines, double time, Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection)
        {
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            //_model = _model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time));
            //_model = temp;

            _shader.SetMatrix4("model", _model);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);


            if (_indices.Count != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
            }
            else
            {

                if (_lines == 0)
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count);
                }
                else if (_lines == 1)
                {
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertices.Count);
                }
                else if (_lines == 2)
                {

                }
                else if (_lines == 3)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, _vertices.Count);
                }
            }
            foreach (var item in Child)
            {
                item.render(_lines, time, temp, camera_view, camera_projection);
            }
        }

        public Vector3 getRad()
        {
            return rad;
        }

        public void translate(float x, float y, float z)
        {
            _model *= Matrix4.CreateTranslation(x, y, z);
            _centerPosition.X += x;
            _centerPosition.Y += y;
            _centerPosition.Z += z;
        }
        public Vector3 getTranslateResult(float x, float y, float z)
        {
            float xTrans = _centerPosition.X + x;
            float yTrans = _centerPosition.Y + y;
            float zTrans = _centerPosition.Z + z;
            Vector3 transResult = new Vector3(xTrans, yTrans, zTrans);
            return transResult;
        }
        public Vector3 getMinCoord()
        {
            return minCoord;
        }
        public Vector3 getMaxCoord()
        {
            return maxCoord;
        }
        public void createBoxVertices(float x, float y, float z, float length)
        {
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 temp_vector;

            //TITIK 1
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 2
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 3
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 4
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 5
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 6
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 7
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 8
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);

            _indices = new List<uint>
            {
                //SEGITIGA DEPAN 1
                0,1,2,
                //SEGITIGA DEPAN 2
                1,2,3,
                //SEGITIGA ATAS 1
                0,4,5,
                //SEGITIGA ATAS 2
                0,1,5,
                //SEGITIGA KANAN 1
                1,3,5,
                //SEGITIGA KANAN 2
                3,5,7,
                //SEGITIGA KIRI 1
                0,2,4,
                //SEGITIGA KIRI 2
                2,4,6,
                //SEGITIGA BELAKANG 1
                4,5,6,
                //SEGITIGA BELAKANG 2
                5,6,7,
                //SEGITIGA BAWAH 1
                2,3,6,
                //SEGITIGA BAWAH 2
                3,6,7
            };


        }


        
        public void rotatede(Vector3 pivot, Vector3 vector, float angle)
        {
            var radAngle = MathHelper.DegreesToRadians(angle);

            var arbRotationMatrix = new Matrix4
                (
                new Vector4((float)(Math.Cos(radAngle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) + vector.Z * Math.Sin(radAngle)), (float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.Y * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) - vector.Z * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.X * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.Y * Math.Sin(radAngle)), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.X * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(radAngle))), 0),
                Vector4.UnitW
                );

            _model *= Matrix4.CreateTranslation(-pivot);
            _model *= arbRotationMatrix;
            _model *= Matrix4.CreateTranslation(pivot);

            for (int i = 0; i < 3; i++)
            {
                _euler[i] = Vector3.Normalize(getRotationResult(pivot, vector, radAngle, _euler[i], true));
            }

            _centerPosition = getRotationResult(pivot, vector, radAngle, _centerPosition);


            foreach (var i in Child)
            {
                i.rotate(pivot, vector, angle);
            }
        }
        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            //pivot -> mau rotate di titik mana
            //vector -> mau rotate di sumbu apa? (x,y,z)
            //angle -> rotatenya berapa derajat?
            var real_angle = angle;
            angle = MathHelper.DegreesToRadians(angle);

            //mulai ngerotasi
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = getRotationResult(pivot, vector, angle, _vertices[i]);
            }
            //rotate the euler direction
            for (int i = 0; i < 3; i++)
            {
                _euler[i] = getRotationResult(pivot, vector, angle, _euler[i], true);

                //NORMALIZE
                //LANGKAH - LANGKAH
                //length = akar(x^2+y^2+z^2)
                float length = (float)Math.Pow(Math.Pow(_euler[i].X, 2.0f) + Math.Pow(_euler[i].Y, 2.0f) + Math.Pow(_euler[i].Z, 2.0f), 0.5f);
                Vector3 temporary = new Vector3(0, 0, 0);
                temporary.X = _euler[i].X / length;
                temporary.Y = _euler[i].Y / length;
                temporary.Z = _euler[i].Z / length;
                _euler[i] = temporary;
            }
            //_centerPosition = getRotationResult(pivot, vector, angle, _centerPosition);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes,
                _vertices.ToArray(), BufferUsageHint.StaticDraw);
            foreach (var item in Child)
            {
                item.rotate(pivot, vector, real_angle);
            }
        }
        public Vector3 getRotationResult(Vector3 pivot, Vector3 vector, float angle, Vector3 point, bool isEuler = false)
        {
            Vector3 temp, newPosition;

            if (isEuler)
            {
                temp = point;
            }
            else
            {
                temp = point - pivot;
            }

            newPosition.X =
                temp.X * (float)(Math.Cos(angle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Y * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) - vector.Z * Math.Sin(angle)) +
                temp.Z * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) + vector.Y * Math.Sin(angle));

            newPosition.Y =
                temp.X * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) + vector.Z * Math.Sin(angle)) +
                temp.Y * (float)(Math.Cos(angle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Z * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) - vector.X * Math.Sin(angle));

            newPosition.Z =
                temp.X * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) - vector.Y * Math.Sin(angle)) +
                temp.Y * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) + vector.X * Math.Sin(angle)) +
                temp.Z * (float)(Math.Cos(angle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(angle)));

            if (isEuler)
            {
                temp = newPosition;
            }
            else
            {
                temp = newPosition + pivot;
            }
            return temp;
        }

        public void resetEuler()
        {
            _euler[0] = new Vector3(1, 0, 0);
            _euler[1] = new Vector3(0, 1, 0);
            _euler[2] = new Vector3(0, 0, 1);
        }
        public void addChild(float x, float y, float z, float length)
        {
            Asset3d newChild = new Asset3d();
            newChild.createBoxVertices2(x, y, z, length);
            Child.Add(newChild);
        }

        

        public void setFragVariable(Vector3 ObjectColor, Vector3 LightColor)
        {
            _shader.SetVector3("objectColor", ObjectColor);
            _shader.SetVector3("lightColor", LightColor);
            //Base Diffuse,Specular,Ambient Power
            setDiffPower(new Vector3(0.1f, 0.1f, 0.1f));
            setSpecPower(0.0f);
            setAmbiPower(0.1f);
            foreach (var item in Child)
            {
                item.setFragVariable(ObjectColor, LightColor);
            }
        }
        public void setFragVariable2(Vector3 LightColor)
        {
            _shader.SetVector3("lightColor", LightColor);
        }

        public void setSpot(Vector3 spotDir,float spotAngle)
        {
            _shader.SetVector3("spotDir", spotDir);
            _shader.SetFloat("spotAngle",(float)(MathHelper.Cos(MathHelper.DegreesToRadians(spotAngle))));
        }

        public void setDiffPower(Vector3 diffPower)
        {
            _shader.SetVector3("diffPow", diffPower);
        }
        public void setSpecPower(float specPower)
        {
            _shader.SetFloat("specPow",specPower);
        }
        public void setAmbiPower(float ambiPower)
        {
            _shader.SetFloat("ambiPow",ambiPower);
        }
        public void setSpecularDiffuseVariable(Vector3 LightPos, Vector3 LightPos2, Vector3 CameraPos)
        {
            //Console.Write(LightPos);
            _shader.SetVector3("bulanPos", LightPos);
            _shader.SetVector3("senterPos", LightPos2);
            _shader.SetVector3("viewPos", CameraPos);
            foreach (var item in Child)
            {
                item.setSpecularDiffuseVariable(LightPos,LightPos2, CameraPos);
            }
        }
        public void createBoxVertices2(float x, float y, float z, float length)
        {
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 temp_vector;
            minCoord = new Vector3(x - length / 2, y - length / 2, z - length / 2);
            maxCoord = new Vector3(x + length / 2, y + length / 2, z + length / 2);
            rad = new Vector3(length / 2, length / 2, length / 2);

            //FRONT FACE

            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));


            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            //BACK FACE
            //TITIK 5
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 6
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));
            //TITIK 7
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 6
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));
            //TITIK 7
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 8
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //LEFT FACE
            //TITIK 1
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 3
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 3
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));

            //RIGHT FACE
            //TITIK 2
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 8
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));

            //BOTTOM FACES
            //TITIK 3
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 8
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));

            //TOP FACES
            //TITIK 1
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 2
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 2
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
        }

        public void createPagar(float x, float y, float z)
        {
            createCustom(x, y + 0.01f, z, 0.08f, 0.01f, 0.01f);
            createCustom(x, y - 0.01f, z, 0.08f, 0.01f, 0.01f);
            createCustom(x + 0.02f, y, z, 0.01f, 0.06f, 0.01f);
            createCustom(x - 0.02f, y, z, 0.01f, 0.06f, 0.01f);
        }

        public void createPagar2(float x, float y, float z)
        {
            createCustom(x, y + 0.01f, z, 0.01f, 0.01f, 0.08f);
            createCustom(x, y - 0.01f, z, 0.01f, 0.01f, 0.08f);
            createCustom(x, y, z + 0.02f, 0.01f, 0.06f, 0.01f);
            createCustom(x, y, z - 0.02f, 0.01f, 0.06f, 0.01f);
        }

        public void createKandang(float x,float y, float z)
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

        public void createBabi(float x, float y, float z)
        {
            createCustom(x, y, z,0.025f,0.025f,0.05f);
            createCustom(x, y + 0.006f, z - 0.029f, 0.025f, 0.025f, 0.02f);
            createCustom(x, y + 0.005f, z - 0.039f, 0.01f, 0.006f, 0.004f);
            createCustom(x - 0.006f, y - 0.02f, z - 0.02f, 0.01f, 0.02f, 0.01f);
            createCustom(x - 0.006f, y - 0.02f, z + 0.02f, 0.01f, 0.02f, 0.01f);
            createCustom(x + 0.006f, y - 0.02f, z - 0.02f, 0.01f, 0.02f, 0.01f);
            createCustom(x + 0.006f, y - 0.02f, z + 0.02f, 0.01f, 0.02f, 0.01f);
        }

        public void createAksesorisBabi(float x, float y, float z)
        {
            createCustom(x - 0.003f, y + 0.005f, z - 0.041f, 0.003f, 0.003f, 0.001f);
            createCustom(x + 0.003f, y + 0.005f, z - 0.041f, 0.003f, 0.003f, 0.001f);
            createCustom(x + 0.006f, y + 0.01f, z - 0.039f, 0.003f, 0.0035f, 0.001f);
            createCustom(x - 0.006f, y + 0.01f, z - 0.039f, 0.003f, 0.0035f, 0.001f);
        }

        public void createAnjing(float x, float y, float z)
        {
            createCustom(x, y, z, 0.025f, 0.025f, 0.05f);
            createCustom(x, y + 0.014f, z - 0.024f, 0.025f, 0.02f, 0.014f);
            createCustom(x, y + 0.004f, z - 0.032f, 0.012f, 0.012f, 0.015f);
            createCustom(x + 0.01f, y + 0.019f, z - 0.024f, 0.009f, 0.01f, 0.01f);
            createCustom(x - 0.01f, y + 0.019f, z - 0.024f, 0.009f, 0.01f, 0.01f);
            createCustom(x - 0.007f, y - 0.02f, z - 0.02f, 0.01f, 0.02f, 0.01f);
            createCustom(x - 0.007f, y - 0.02f, z + 0.02f, 0.01f, 0.02f, 0.01f);
            createCustom(x + 0.007f, y - 0.02f, z - 0.02f, 0.01f, 0.02f, 0.01f);
            createCustom(x + 0.007f, y - 0.02f, z + 0.02f, 0.01f, 0.02f, 0.01f);
        }

        public void createAksesorisAnjing(float x, float y, float z)
        {
            createCustom(x, y + 0.0085f, z - 0.0385f, 0.0125f, 0.004f, 0.003f);
            createCustom(x, y + 0.005f, z - 0.0385f, 0.004f, 0.004f, 0.003f);
            createCustom(x + 0.007f, y + 0.013f, z - 0.03f, 0.004f, 0.004f, 0.003f);
            createCustom(x - 0.007f, y + 0.013f, z - 0.03f, 0.004f, 0.004f, 0.003f);
        }

        public void createPanda1(float x, float y, float z)
        {   //badan
            //createCustom(x-0.01f, y+0.01f, z - 0.05f, 0.06f, 0.05f, 0.08f);
            createCustom(x - 0.01f, y + 0.01f, z - 0.05f, 0.05f, 0.04f, 0.02f);
            createCustom(x - 0.01f, y + 0.01f, z - 0.09f, 0.05f, 0.04f, 0.02f);
            //kepala
            createCustom(x - 0.01f, y + 0.01f, z - 0.1f, 0.03f, 0.03f, 0.02f);
            //hidung
            createCustom(x - 0.01f, y + 0.005f, z - 0.111f, 0.015f, 0.01f, 0.005f);
            // mata kiri
            createCustom(x + 0.001f, y + 0.01f, z - 0.11f, 0.004f, 0.004f, 0.0015f);
            // mata kanan
            createCustom(x - 0.019f, y + 0.01f, z - 0.11f, 0.004f, 0.004f, 0.0015f);
        }

        public void createPanda2(float x, float y, float z)
        {
            //badan tengah
            createCustom(x - 0.01f, y + 0.01f, z - 0.07f, 0.05f, 0.04f, 0.02f);
            //telinga kanan
            createCustom(x - 0.025f, y + 0.025f, z - 0.105f, 0.01f, 0.01f, 0.0045f);
            //telinga kiri
            createCustom(x + 0.005f, y + 0.025f, z - 0.105f, 0.01f, 0.01f, 0.0045f);
            //lingkaran mata kiri
            createCustom(x + 0.00035f, y + 0.01f, z - 0.11f, 0.01f, 0.01f, 0.0015f);
            //lingkaran mata kanan
            createCustom(x - 0.019f, y + 0.01f, z - 0.11f, 0.01f, 0.01f, 0.001f);
            //lingkaran hidung
            createCustom(x - 0.01f, y + 0.005f, z - 0.115f, 0.005f, 0.0025f, 0.0015f);
            //kaki 
            createCustom(x - 0.025f, y - 0.02f, z - 0.055f, 0.015f, 0.02f, 0.015f);
            createCustom(x + 0.005f, y - 0.02f, z - 0.055f, 0.015f, 0.02f, 0.015f);
            createCustom(x - 0.025f, y - 0.02f, z - 0.085f, 0.015f, 0.02f, 0.015f);
            createCustom(x + 0.005f, y - 0.02f, z - 0.085f, 0.015f, 0.02f, 0.015f);

        }

        public void createBear1(float x, float y, float z)
        {   //badan
            createCustom(x + 0.3f, y + 0.01f, z - 0.07f, 0.05f, 0.04f, 0.06f);
            /* createCustom(x - 0.01f, y + 0.01f, z - 0.05f, 0.05f, 0.04f, 0.02f);
             createCustom(x - 0.01f, y + 0.01f, z - 0.09f, 0.05f, 0.04f, 0.02f);*/
            //kepala
            createCustom(x + 0.3f, y + 0.01f, z - 0.1f, 0.03f, 0.03f, 0.02f);
            //hidung
            createCustom(x + 0.3f, y + 0.005f, z - 0.111f, 0.015f, 0.01f, 0.005f);
            //kaki 
            createCustom(x + 0.31f, y - 0.02f, z - 0.05f, 0.015f, 0.02f, 0.015f);
            createCustom(x + 0.29f, y - 0.02f, z - 0.05f, 0.015f, 0.02f, 0.015f);
            createCustom(x + 0.31f, y - 0.02f, z - 0.08f, 0.015f, 0.02f, 0.015f);
            createCustom(x + 0.29f, y - 0.02f, z - 0.08f, 0.015f, 0.02f, 0.015f);
            //telinga kanan
            createCustom(x + 0.315f, y + 0.025f, z - 0.105f, 0.01f, 0.01f, 0.0045f);
            //telinga kiri
            createCustom(x + 0.285f, y + 0.025f, z - 0.105f, 0.01f, 0.01f, 0.0045f);
        }

        public void createBear2(float x, float y, float z)
        {

            //lingkaran hidung
            createCustom(x + 0.3f, y + 0.005f, z - 0.115f, 0.005f, 0.0025f, 0.0015f);
            // mata kiri
            createCustom(x + 0.29f, y + 0.01f, z - 0.11f, 0.004f, 0.004f, 0.0015f);
            // mata kanan
            createCustom(x + 0.31f, y + 0.01f, z - 0.11f, 0.004f, 0.004f, 0.0015f);

        }
        //Bebek
        public void createBebek1(float x, float y, float z)
        {
            //badan
            createCustom(x + 0.6f, y - 0.01f, z - 0.07f, 0.02f, 0.02f, 0.02f);
            //sayap
            createCustom(x + 0.59f, y - 0.0055f, z - 0.07f, 0.005f, 0.01f, 0.015f);
            createCustom(x + 0.61f, y - 0.0055f, z - 0.07f, 0.005f, 0.01f, 0.015f);
            //kepala
            createCustom(x + 0.6f, y, z - 0.08f, 0.01f, 0.02f, 0.01f);
        }
        public void createBebek2(float x, float y, float z)
        {
            //leg
            createCustom(x + 0.605f, y - 0.02f, z - 0.07f, 0.003f, 0.02f, 0.003f);
            createCustom(x + 0.595f, y - 0.02f, z - 0.07f, 0.003f, 0.02f, 0.003f);
            //feet
            createCustom(x + 0.605f, y - 0.03f, z - 0.071f, 0.005f, 0.001f, 0.005f);
            createCustom(x + 0.595f, y - 0.03f, z - 0.071f, 0.005f, 0.001f, 0.005f);
            //mulut
            createCustom(x + 0.6f, y, z - 0.087f, 0.01f, 0.004f, 0.005f);
        }
        public void createBebek3(float x, float y, float z)
        {
            //mata
            createCustom(x + 0.603f, y + 0.005f, z - 0.085f, 0.0025f, 0.0025f, 0.001f);
            createCustom(x + 0.597f, y + 0.005f, z - 0.085f, 0.0025f, 0.0025f, 0.001f);
        }

        public void createPolarBear1(float x, float y, float z)
        {   //badan
            createCustom(x + 1.2f, y + 0.01f, z - 0.07f, 0.05f, 0.04f, 0.06f);
            /* createCustom(x - 0.01f, y + 0.01f, z - 0.05f, 0.05f, 0.04f, 0.02f);
             createCustom(x - 0.01f, y + 0.01f, z - 0.09f, 0.05f, 0.04f, 0.02f);*/
            //kepala
            createCustom(x + 1.2f, y + 0.01f, z - 0.1f, 0.03f, 0.03f, 0.02f);
            //hidung
            createCustom(x + 1.2f, y + 0.005f, z - 0.111f, 0.015f, 0.01f, 0.005f);
            //kaki 
            createCustom(x + 1.21f, y - 0.02f, z - 0.05f, 0.015f, 0.02f, 0.015f);
            createCustom(x + 1.19f, y - 0.02f, z - 0.05f, 0.015f, 0.02f, 0.015f);
            createCustom(x + 1.21f, y - 0.02f, z - 0.08f, 0.015f, 0.02f, 0.015f);
            createCustom(x + 1.19f, y - 0.02f, z - 0.08f, 0.015f, 0.02f, 0.015f);
            //telinga
            createCustom(x + 1.215f, y + 0.025f, z - 0.105f, 0.01f, 0.01f, 0.0045f);
            createCustom(x + 1.185f, y + 0.025f, z - 0.105f, 0.01f, 0.01f, 0.0045f);
        }

        public void createPolarBear2(float x, float y, float z)
        {

            //lingkaran hidung
            createCustom(x + 1.2f, y + 0.005f, z - 0.115f, 0.005f, 0.0025f, 0.0015f);
            // mata kiri
            createCustom(x + 1.19f, y + 0.01f, z - 0.11f, 0.004f, 0.004f, 0.0015f);
            // mata kanan
            createCustom(x + 1.21f, y + 0.01f, z - 0.11f, 0.004f, 0.004f, 0.0015f);

        }
        //llama
        public void createLlama1(float x, float y, float z)
        {   //badan
            createCustom(x + 1.5f, y + 0.03f, z - 0.07f, 0.04f, 0.03f, 0.06f);
            //kepala
            createCustom(x + 1.5f, y + 0.06f, z - 0.1f, 0.025f, 0.06f, 0.02f);
            //hidung
            createCustom(x + 1.5f, y + 0.075f, z - 0.111f, 0.01f, 0.01f, 0.02f);
            //kaki 
            createCustom(x + 1.51f, y - 0.0f, z - 0.05f, 0.015f, 0.05f, 0.015f);
            createCustom(x + 1.49f, y - 0.0f, z - 0.05f, 0.015f, 0.05f, 0.015f);
            createCustom(x + 1.51f, y - 0.0f, z - 0.08f, 0.015f, 0.05f, 0.015f);
            createCustom(x + 1.49f, y - 0.0f, z - 0.08f, 0.015f, 0.05f, 0.015f);
            //telinga
            createCustom(x + 1.507f, y + 0.09f, z - 0.105f, 0.008f, 0.02f, 0.005f);
            createCustom(x + 1.492f, y + 0.09f, z - 0.105f, 0.008f, 0.02f, 0.005f);
        }

        public void createLlama2(float x, float y, float z)
        {
            // mata
            createCustom(x + 1.493f, y + 0.078f, z - 0.11f, 0.004f, 0.004f, 0.0015f);
            createCustom(x + 1.507f, y + 0.078f, z - 0.11f, 0.004f, 0.004f, 0.0015f);
            // hidung
            createCustom(x + 1.497f, y + 0.075f, z - 0.121f, 0.003f, 0.003f, 0.0015f);
            createCustom(x + 1.503f, y + 0.075f, z - 0.121f, 0.003f, 0.003f, 0.0015f);
        }
        public void createLlama3(float x, float y, float z)
        {
            // alis
            createCustom(x + 1.493f, y + 0.081f, z - 0.11f, 0.004f, 0.002f, 0.0015f);
            createCustom(x + 1.507f, y + 0.081f, z - 0.11f, 0.004f, 0.002f, 0.0015f);
            // hidung
            createCustom(x + 1.5f, y + 0.072f, z - 0.121f, 0.005f, 0.003f, 0.0015f);
            //telinga
            createCustom(x + 1.507f, y + 0.085f, z - 0.107f, 0.004f, 0.02f, 0.002f);
            createCustom(x + 1.492f, y + 0.085f, z - 0.1071f, 0.004f, 0.02f, 0.002f);
        }
        //camel
        public void createCamel1(float x, float y, float z)
        {   //badan
            createCustom(x + 1.8f, y + 0.03f, z - 0.07f, 0.04f, 0.03f, 0.06f);
            //punggung
            createCustom(x + 1.8f, y + 0.04f, z - 0.06f, 0.03f, 0.03f, 0.045f);
            createCustom(x + 1.8f, y + 0.05f, z - 0.06f, 0.025f, 0.025f, 0.035f);
            //kepala
            createCustom(x + 1.8f, y + 0.06f, z - 0.1f, 0.025f, 0.06f, 0.02f);
            //hidung
            createCustom(x + 1.8f, y + 0.075f, z - 0.111f, 0.01f, 0.01f, 0.02f);
            //kaki 
            createCustom(x + 1.81f, y - 0.0f, z - 0.05f, 0.015f, 0.05f, 0.015f);
            createCustom(x + 1.79f, y - 0.0f, z - 0.05f, 0.015f, 0.05f, 0.015f);
            createCustom(x + 1.81f, y - 0.0f, z - 0.08f, 0.015f, 0.05f, 0.015f);
            createCustom(x + 1.79f, y - 0.0f, z - 0.08f, 0.015f, 0.05f, 0.015f);
            //telinga
            createCustom(x + 1.807f, y + 0.09f, z - 0.105f, 0.008f, 0.02f, 0.005f);
            createCustom(x + 1.792f, y + 0.09f, z - 0.105f, 0.008f, 0.02f, 0.005f);
        }

        public void createCamel2(float x, float y, float z)
        {
            // mata
            createCustom(x + 1.793f, y + 0.078f, z - 0.11f, 0.004f, 0.004f, 0.0015f);
            createCustom(x + 1.807f, y + 0.078f, z - 0.11f, 0.004f, 0.004f, 0.0015f);
            // hidung
            createCustom(x + 1.797f, y + 0.075f, z - 0.121f, 0.003f, 0.003f, 0.0015f);
            createCustom(x + 1.803f, y + 0.075f, z - 0.121f, 0.003f, 0.003f, 0.0015f);
        }
        public void createCamel3(float x, float y, float z)
        {
            // alis
            createCustom(x + 1.793f, y + 0.081f, z - 0.11f, 0.005f, 0.002f, 0.0015f);
            createCustom(x + 1.807f, y + 0.081f, z - 0.11f, 0.005f, 0.002f, 0.0015f);

            //telinga
            createCustom(x + 1.807f, y + 0.085f, z - 0.107f, 0.004f, 0.02f, 0.002f);
            createCustom(x + 1.792f, y + 0.085f, z - 0.1071f, 0.004f, 0.02f, 0.002f);
        }
        //sheep
        public void createSheep1(float x, float y, float z)
        {   //badan
            createCustom(x + 2.1f, y + 0.015f, z - 0.07f, 0.04f, 0.03f, 0.06f);
            //kepala
            createCustom(x + 2.1f, y + 0.025f, z - 0.1f, 0.03f, 0.025f, 0.025f);

            //kaki 
            createCustom(x + 2.11f, y - 0.005f, z - 0.05f, 0.015f, 0.01f, 0.015f);
            createCustom(x + 2.09f, y - 0.005f, z - 0.05f, 0.015f, 0.01f, 0.015f);
            createCustom(x + 2.11f, y - 0.005f, z - 0.08f, 0.015f, 0.01f, 0.015f);
            createCustom(x + 2.09f, y - 0.005f, z - 0.08f, 0.015f, 0.01f, 0.015f);
            //telinga
            createCustom(x + 2.12f, y + 0.025f, z - 0.105f, 0.008f, 0.004f, 0.003f);
            createCustom(x + 2.08f, y + 0.025f, z - 0.105f, 0.008f, 0.004f, 0.003f);

        }

        public void createSheep2(float x, float y, float z)
        {
            //hidung
            createCustom(x + 2.1f, y + 0.02f, z - 0.111f, 0.01f, 0.01f, 0.01f);
            //kaki 
            createCustom(x + 2.11f, y - 0.0f, z - 0.05f, 0.005f, 0.05f, 0.005f);
            createCustom(x + 2.09f, y - 0.0f, z - 0.05f, 0.005f, 0.05f, 0.005f);
            createCustom(x + 2.11f, y - 0.0f, z - 0.08f, 0.005f, 0.05f, 0.005f);
            createCustom(x + 2.09f, y - 0.0f, z - 0.08f, 0.005f, 0.05f, 0.005f);
            //telinga
            createCustom(x + 2.12f, y + 0.0215f, z - 0.105f, 0.007f, 0.004f, 0.003f);
            createCustom(x + 2.08f, y + 0.0215f, z - 0.105f, 0.007f, 0.004f, 0.003f);
        }
        public void createSheep3(float x, float y, float z)
        {
            // mata
            createCustom(x + 2.093f, y + 0.025f, z - 0.114f, 0.004f, 0.004f, 0.0015f);
            createCustom(x + 2.107f, y + 0.025f, z - 0.114f, 0.004f, 0.004f, 0.0015f);

        }
        public void createRedPanda1(float x, float y, float z)
        {
            //badan
            createCustom(x + 0.9f, y - 0.015f, z - 0.095f, 0.025f, 0.025f, 0.045f);
            //ekor
            createCustom(x + 0.9f, y - 0.015f, z - 0.07f, 0.015f, 0.015f, 0.01f);
            createCustom(x + 0.9f, y - 0.015f, z - 0.0583f, 0.015f, 0.015f, 0.0045f);
            createCustom(x + 0.9f, y - 0.015f, z - 0.0495f, 0.015f, 0.015f, 0.0045f);
            //kepala
            createCustom(x + 0.9f, y - 0.015f, z - 0.125f, 0.023f, 0.02f, 0.016f);
        }
        public void createRedPanda2(float x, float y, float z)
        {
            //ekor
            createCustom(x + 0.9f, y - 0.015f, z - 0.0627f, 0.015f, 0.015f, 0.0045f);
            createCustom(x + 0.9f, y - 0.015f, z - 0.054f, 0.015f, 0.015f, 0.0045f);
            createCustom(x + 0.9f, y - 0.015f, z - 0.045f, 0.015f, 0.015f, 0.0045f);
            //telinga
            createCustom(x + 0.909f, y - 0.007f, z - 0.125f, 0.01f, 0.01f, 0.003f);
            createCustom(x + 0.89f, y - 0.007f, z - 0.125f, 0.01f, 0.01f, 0.003f);
            //hidung
            createCustom(x + 0.9f, y - 0.02f, z - 0.13f, 0.015f, 0.009f, 0.018f);
            // pipi 
            createCustom(x + 0.9099f, y - 0.018f, z - 0.132f, 0.004f, 0.015f, 0.0025f);
            createCustom(x + 0.89f, y - 0.018f, z - 0.132f, 0.004f, 0.015f, 0.0025f);
        }
        public void createRedPanda3(float x, float y, float z)
        {
            //ekor
            createCustom(x + 0.9f, y - 0.015f, z - 0.0403f, 0.015f, 0.015f, 0.005f);
            //kaki
            createCustom(x + 0.907f, y - 0.03f, z - 0.08f, 0.01f, 0.01f, 0.01f);
            createCustom(x + 0.893f, y - 0.03f, z - 0.08f, 0.01f, 0.01f, 0.01f);
            createCustom(x + 0.907f, y - 0.03f, z - 0.11f, 0.01f, 0.01f, 0.01f);
            createCustom(x + 0.893f, y - 0.03f, z - 0.11f, 0.01f, 0.01f, 0.01f);
            //mata
            createCustom(x + 0.905f, y - 0.013f, z - 0.132f, 0.004f, 0.004f, 0.004f);
            createCustom(x + 0.895f, y - 0.013f, z - 0.132f, 0.004f, 0.004f, 0.004f);
            //hidung
            createCustom(x + 0.9f, y - 0.017f, z - 0.139f, 0.004f, 0.004f, 0.001f);

        }
        public void createTree1(float x, float y, float z)
        {
            createCustom(x - 0.9f, y + 0.017f, z - 0.1f, 0.05f, 0.2f, 0.05f);
            createCustom(x - 0.9f, y + 0.12f, z - 0.07f, 0.035f, 0.07f, 0.04f);
            createCustom(x - 0.9f, y + 0.06f, z - 0.12f, 0.03f, 0.02f, 0.09f);
            createCustom(x - 0.9f, y + 0.09f, z - 0.155f, 0.025f, 0.04f, 0.02f);
        }
        public void createTree2(float x, float y, float z)
        {
            createCustom(x - 0.9f, y + 0.17f, z - 0.07f, 0.2f, 0.04f, 0.2f);
            createCustom(x - 0.9f, y + 0.2f, z - 0.07f, 0.15f, 0.05f, 0.15f);
            createCustom(x - 0.9f, y + 0.23f, z - 0.07f, 0.1f, 0.05f, 0.1f);
            createCustom(x - 0.9f, y + 0.26f, z - 0.07f, 0.05f, 0.05f, 0.05f);

            createCustom(x - 0.9f, y + 0.1f, z - 0.16f, 0.05f, 0.02f, 0.065f);
            createCustom(x - 0.9f, y + 0.12f, z - 0.16f, 0.045f, 0.02f, 0.05f);
        }

        public void createTreeBundar1(float x, float y, float z)
        {
            createCustom(x - 1.2f, y + 0.017f, z - 0.1f, 0.05f, 0.2f, 0.05f);
            createCustom(x - 1.2f, y + 0.12f, z - 0.07f, 0.035f, 0.07f, 0.04f);
            createCustom(x - 1.2f, y + 0.06f, z - 0.12f, 0.03f, 0.02f, 0.09f);
            createCustom(x - 1.2f, y + 0.09f, z - 0.155f, 0.025f, 0.04f, 0.02f);
        }
        public void createTreeBundar2(float x, float y, float z)
        {
            createCustom(x - 1.2f, y + 0.17f, z - 0.07f, 0.15f, 0.04f, 0.15f);
            createCustom(x - 1.2f, y + 0.2f, z - 0.07f, 0.16f, 0.05f, 0.16f);
            createCustom(x - 1.2f, y + 0.23f, z - 0.07f, 0.15f, 0.05f, 0.15f);
            createCustom(x - 1.2f, y + 0.26f, z - 0.07f, 0.15f, 0.05f, 0.1f);

            createCustom(x - 1.2f, y + 0.1f, z - 0.16f, 0.05f, 0.02f, 0.065f);
            createCustom(x - 1.2f, y + 0.12f, z - 0.16f, 0.045f, 0.02f, 0.05f);
        }
        public void createCustom(float x, float y, float z, float lengthX, float lengthY, float lengthZ)
        {
            rad = new Vector3(lengthX / 2, lengthY / 2, lengthZ / 2);
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 temp_vector;

            minCoord = new Vector3(x - lengthX / 2, y - lengthY / 2, z - lengthZ / 2);
            maxCoord = new Vector3(x + lengthX / 2, y + lengthY / 2, z + lengthZ / 2);

            //FRONT FACE

            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));


            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            //BACK FACE
            //TITIK 5
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 6
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));
            //TITIK 7
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 6
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));
            //TITIK 7
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 8
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //LEFT FACE
            //TITIK 1
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 3
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 3
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));

            //RIGHT FACE
            //TITIK 2
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 8
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));

            //BOTTOM FACES
            //TITIK 3
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 8
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y - lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));

            //TOP FACES
            //TITIK 1
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 2
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 2
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z - lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + lengthX / 2.0f;
            temp_vector.Y = y + lengthY / 2.0f;
            temp_vector.Z = z + lengthZ / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
        }


        public void load_withnormal(string shadervert, string shaderfrag, float Size_x, float Size_y)
        {
            //Buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count
                * Vector3.SizeInBytes, _vertices.ToArray(), BufferUsageHint.StaticDraw);
            //VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float,
                false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float,
                false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            //if (_indices.Count != 0)
            //{
            //    _elementBufferObject = GL.GenBuffer();
            //    GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            //    GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count
            //        * sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);
            //}
            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();
            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size_x / (float)Size_y, 0.1f, 100.0f);
            foreach (var item in Child)
            {
                item.load_withnormal(shadervert, shaderfrag, Size_x, Size_y);
            }
        }
    }
}