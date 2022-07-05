using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace _3D_Object
{
    internal class Asset2d
    {
        float[] vertices =
        {

        };
        uint[] indices =
        {

        };

        float[] colors =
        {

        };
        public Asset2d(float[] vertice, uint[] indice, float[] color)
        {
            vertices = vertice;
            indices = indice;
            colors = color;
            indexs = 0;
        }
        int vertexBufferObject;
        //mengatur susunan segitiga kalau mau bikin kotak dari segitiga
        int elemenBufferObject;
        int vertexArrayObject; Shader shader;

        int indexs;
        int[] pascal;

        public void load(string shadervert, string shaderfrag)
        {

            vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);


            GL.EnableVertexAttribArray(0);

            if (indices.Length != 0)
            {
                elemenBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elemenBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer,
                    indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            }
            shader = new Shader(shadervert, shaderfrag);
            shader.Use();

        }
        public void render2d(int pilihan)
        {
            shader.Use();


            if (colors.Length > 0)
            {
                int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
                GL.Uniform4(vertexColorLocation, colors[0], colors[1], colors[2], colors[3]);

            };
            GL.BindVertexArray(vertexArrayObject);
            if (indices.Length != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, indices.Length,
                 DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                if (pilihan == 0)
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                }
                else if (pilihan == 1)
                {
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, (vertices.Length + 1) / 3);
                }
                else if (pilihan == 2)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, indexs);
                }
                else if (pilihan == 3)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, (vertices.Length + 1) / 3);
                }

            }

        }
        public List<int> getRow(int rowIndex)
        {
            List<int> currow = new List<int>();
            currow.Add(1);
            if (rowIndex == 0)
            {
                return currow;
            }
            List<int> prev = getRow(rowIndex - 1);

            for (int i = 1; i < prev.Count; i++)
            {
                int curr = prev[i - 1] + prev[i];
                currow.Add(curr);
            }
            currow.Add(1);
            return currow;
        }
        public Vector2 getP(int n, float t)
        {
            Vector2 p = new Vector2(0, 0);
            float k;
            for (int i = 0; i < n; i++)
            {
                k = (float)Math.Pow(1 - t, n - 1 - i) * (float)Math.Pow(t, i) * pascal[i];
                p.X += k * vertices[i * 3];
                p.Y += k * vertices[i * 3 + 1];

            }
            return p;
        }

        public List<float> createCurveBezier(float x, float y)
        {
            vertices[indexs * 3] = x;
            vertices[indexs * 3 + 1] = y;
            vertices[indexs * 3 + 2] = 0;
            indexs++;

            GL.BufferData(BufferTarget.ArrayBuffer,
                vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            List<float> verticesBezier = new List<float>();
            List<int> pscl = getRow(indexs - 1);
            pascal = pscl.ToArray();
            for (float t = 0; t <= 1.0f; t += 0.01f)
            {
                Vector2 p = getP(indexs, t);
                verticesBezier.Add(p.X);
                verticesBezier.Add(p.Y);
                verticesBezier.Add(0);
            }
            return verticesBezier;
        }
        public bool getVerticesLength()
        {
            if (vertices[0] == 0)
            {
                return false;
            }
            if ((vertices.Length + 1) / 3 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setVertices(float[] temp)
        {
            vertices = temp;
        }

    }
}
