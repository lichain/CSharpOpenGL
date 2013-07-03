using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using System.IO;

namespace CSharpOpenGL
{
        public enum ConvertType
        {
                TypeA2TypeB,
                TypeB2TypeA
        }

        public enum VertexType
        {
                General,
                InSide,
                OutSide,
                MouseRight,
                Choosing,
        }

        public enum CurrentMode
        {
                GeneralMode,
                EditMode,
        }

        /// <summary>
        /// The main form class.
        /// </summary>
        public partial class SharpGLForm : Form
        {

                OpenGL gl;
                int windows_w = 55;
                int unit = 10;
                int w = 550;
                int h = 550;

                float LineWidth = 3.0f;
                float PointSize = 10f;

                /// 用來判斷是否封閉 v[n] = v[0]
                bool LockFlag = false;
                bool ChoosePointFlag = false;

                bool MouseRightMode = false;
                Point MouseRightPoint = new Point();
                Point JorandEndPoint = new Point();

                int choose_index = -1;
                Point pre_point, choose_point;

                int clickCount = 0;
                int rightClicNum = 0;

                //設定邊界值
                int BoundaryL = 0;
                int BoundaryR = 0;
                int BoundaryT = 0;
                int BoundaryB = 0;

                //點集合
                List<Point> vertices = new List<Point>();

                CurrentMode Mode = CurrentMode.GeneralMode;

                /// <summary>
                /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
                /// </summary>
                public SharpGLForm()
                {
                        InitializeComponent();
                }

                /// <summary>
                /// Handles the OpenGLDraw event of the openGLControl control.
                /// </summary>
                /// <param name="sender">The source of the event.</param>
                /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
                private void openGLControl_OpenGLDraw(object sender, PaintEventArgs e)
                {                        
                        //  Get the OpenGL object.
                        gl = openGLControl.OpenGL;

                        //  Clear the color and depth buffer.
                        gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT );

                        gl.Enable(OpenGL.GL_BLEND);							// Enable Blending
                        gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
                        //  Load the identity matrix.
                        gl.LoadIdentity();

                        gl.LineWidth(1.0f);
                        gl.Color(0.0, 0.0, 0.0);
                        gl.Begin(OpenGL.GL_LINES);

                        for (double i = 0; i <= windows_w; i= i+1.0)
                        {
                                gl.Vertex(-windows_w, i);
                                gl.Vertex(windows_w, i);
                                gl.Vertex(-windows_w, -i);
                                gl.Vertex(windows_w, -i);

                                gl.Vertex(i, windows_w);
                                gl.Vertex(i, -windows_w);
                                gl.Vertex(-i, windows_w);
                                gl.Vertex(-i, -windows_w);
                        }
                        gl.End();

                        if (Mode == CurrentMode.EditMode && pre_point != null && choose_point != null
               && (pre_point.X != choose_point.X || pre_point.Y != choose_point.Y) && choose_index != -1)
                        {
                                if (choose_index == 0)
                                {
                                        SetQUAD(choose_point.X, choose_point.Y, VertexType.Choosing);
                                        DrawLine(vertices[1], choose_point);
                                        DrawLine(choose_point, vertices[vertices.Count-1]);
                                }
                                else {
                                        SetQUAD(choose_point.X, choose_point.Y, VertexType.Choosing);
                                        DrawLine(vertices[choose_index - 1], choose_point);
                                        if(choose_index + 1 >= vertices.Count){
                                                DrawLine(choose_point, vertices[0]);
                                        }
                                        else{
                                                DrawLine(choose_point, vertices[choose_index + 1]);
                                        }

                                        //int X = choose_point.X;
                                        //(Point)vertices[choose_index] = X;
                                        //vertices[choose_index].Y = choose_point.Y;
                                }
                        }


                        if (vertices.Count > 0  )
                        {
                                for (int i = 0; i < vertices.Count; i++ )
                                {
                                        //Draw Point
                                        SetQUAD(vertices[i].X, vertices[i].Y, VertexType.General);

                                        if (Mode == CurrentMode.EditMode && i == vertices.Count - 1)
                                        {
                                                DrawLine(vertices[vertices.Count - 1], vertices[0]);
                                                FillPolygon();
                                        }
                                        //兩個點連成一條線
                                        if (i >= 1 && vertices.Count >= 2)
                                        {
                                                DrawLine(vertices[i - 1], vertices[i]);
                                                if (LockFlag)
                                                {
                                                        DrawLine(vertices[vertices.Count - 1], vertices[0]);
                                                        FillPolygon();
                                                }
                                                if (vertices[i].X == vertices[0].X && vertices[i].Y == vertices[0].Y)
                                                {
                                                        LockFlag = true;
                                                        FillPolygon();
                                                }
                                                else { 
                                                        //做八皇后檢查
                                                        if (vertices.Count >= 4 && CheckRangePoint(vertices[vertices.Count-1], vertices))
                                                        {
                                                                LockFlag = true;
                                                                FillPolygon();
                                                        }
                                                }
                                        }// end if


                                }//end of For
                        }// end if

                        if (MouseRightMode)
                        {
                                //Draw Point
                                SetQUAD(MouseRightPoint.X, MouseRightPoint.Y, VertexType.MouseRight);
                                //Draw Line
                                JorandEndPoint.X = windows_w -1 ;
                                JorandEndPoint.Y = MouseRightPoint.Y ;
                                DrawLine(MouseRightPoint, JorandEndPoint);
                        }

       

                        //  Nudge the rotation.
                        //rotation += 3.0f;
                }

                /// <summary>
                /// Handles the OpenGLInitialized event of the openGLControl control.
                /// </summary>
                /// <param name="sender">The source of the event.</param>
                /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
                private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
                {
                        //  TODO: Initialise OpenGL here.

                        //  Get the OpenGL object.
                        OpenGL gl = openGLControl.OpenGL;

                        //  Set the clear color.
                        gl.ClearColor(1.0f, 1.0f, 1.0f, 0);
                }

                /// <summary>
                /// Handles the Resized event of the openGLControl control.
                /// </summary>
                /// <param name="sender">The source of the event.</param>
                /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
                private void openGLControl_Resized(object sender, EventArgs e)
                {
                        //  TODO: Set the projection matrix here.

                        //  Get the OpenGL object.
                       gl= openGLControl.OpenGL;

                        //  Set the projection matrix.
                        gl.MatrixMode(OpenGL.GL_PROJECTION);

                        //  Load the identity.
                        gl.LoadIdentity();

                        //  Create a perspective transformation.
                        //gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

                        //gl.Ortho(-20, 20, -20, 20, -20, 20); // near z = -9 ,far z = 11 
                        gl.Ortho2D(0, 55, 0, 55);

                        //  Use the 'look at' helper function to position and aim the camera.
                        //gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);
                        gl.LookAt(0, 0, 1,
                        0, 0, -1,
                        0, 1, 0);

                        //  Set the modelview matrix.
                        gl.MatrixMode(OpenGL.GL_MODELVIEW);
                }

                /// <summary>
                /// The current rotation.
                /// </summary>
                private float rotation = 0.0f;

                //加 Vertex
                private void AddVertex(int x, int y)
                {
                        Point v = new Point(x, y);
                        vertices.Add(v);
                }
                
                //畫單點
                private void SetPoint(int x, int y)
                {
                        gl = openGLControl.OpenGL;
                        gl.PointSize(10.0f);
                        gl.Color(1.0f, 0.0f, 0.0f);
                        gl.Begin(OpenGL.GL_POINT);
                                gl.Vertex(x, y,0);
                        gl.End();
                        gl.Flush();
                }

                //畫點
                //VertexType = {General, InSide, OutSide,MouseRight }
                private void SetQUAD(int x, int y,  VertexType type)
                {                       
                        gl = openGLControl.OpenGL;
                        switch (type)
                        {
                                case VertexType.General:
                                        gl.Color(1.0f, 0.0f, 0.0f);
                                        break;
                                case VertexType.InSide:
                                        gl.Color(0.5f, 0.0f, 0.0f,0.1);
                                        break;
                                case VertexType.OutSide:
                                        return;
                                case VertexType.MouseRight:
                                        gl.Color(0.0f, 1.0f, 0.0f, 0.7);
                                        break;
                                case VertexType.Choosing:
                                        gl.Color(0.0f, 1.0f, 1.0f, 0.7);
                                        break;
                        }

                        gl.PointSize(PointSize);
                        gl.Begin(OpenGL.GL_QUADS);
                                gl.Vertex(x, y);
                                gl.Vertex(x + 1, y);
                                gl.Vertex(x + 1, y + 1);
                                gl.Vertex(x, y + 1);
                        gl.End();

                        gl.Flush();
                }

                //畫線
                private void DrawLine(int x1, int y1, int x2, int y2)
                {
                        Point V1 = new Point(x1, y1);
                        Point V2 = new Point(x2, y2);
                        DrawLine(V1, V2);
                }

                //畫線
                private void DrawLine(Point  v1, Point v2)
                {
                        gl = openGLControl.OpenGL;
                        gl.LineWidth(LineWidth);
                        gl.Color(1.0f, 0.0f, 0.0f);
                        gl.Begin(OpenGL.GL_LINES);
                                gl.Vertex(v1.X+.5,v1.Y+.5);
                                gl.Vertex(v2.X+.5, v2.Y+.5);
                        gl.End();
                        gl.Flush();
                }

                //座標轉換(目前只用到滑鼠座標轉成2D座標, only enable ConvertType = TypeA2TypeB)
                private void ConvertCoordinate(ref int x, ref int y, ConvertType type)
                {
                        //y = y - 10;
                        switch (type)
                        {
                                case ConvertType.TypeA2TypeB:
                                        //double fx = Math.Round(((double)x - w / 2)/unit, 0, MidpointRounding.AwayFromZero);
                                        //double fy = (-1) * Math.Round(((double)y - h / 2) / unit, 0, MidpointRounding.AwayFromZero);
                                        x = Convert.ToInt32(x / unit);
                                        y = Convert.ToInt32((windows_w - 1) - y / unit);
                                        break;
                                case ConvertType.TypeB2TypeA:
                                        //x = x + w / 2;
                                        //y = y + h / 2;
                                        break;
                        }
                        
                        //return point;
                }

                //滑鼠移動
                private void openGLControl_MouseMove(object sender, MouseEventArgs e)
                {
                        int X,  Y;
                        X = e.X;
                        Y = e.Y;
                        
                        ConvertCoordinate(ref X, ref Y, ConvertType.TypeA2TypeB);

                        lbMouse.Text = string.Format("Mouse: x={0},y={1}", e.X, e.Y);
                        lbConvert.Text = string.Format("Convert: x={0},y={1}", X, Y);

                        if (pre_point != null && Mode == CurrentMode.EditMode && choose_index != -1)
                        {
                                choose_point.X = X;
                                choose_point.Y = Y;
                                //vertices[choose_index].Y = choose_point.Y;
                        }
                }

                //清除按鈕
                private void btnDraw_Click(object sender, EventArgs e)
                {
                        Reset();
                }

                private void Reset()
                {
                        vertices.Clear();
                        LockFlag = false;
                        MouseRightMode = false;
                        listResult.Items.Clear();
                        listRightClick.Items.Clear();
                        lbConvert.Text = "";
                        lbMouse.Text = "";
                        lbCrossNum.Text = "0";
                        clickCount = 0;
                        rightClicNum = 0;
                        choose_index = -1;
                        Mode = CurrentMode.GeneralMode;
                }

                //Jordan Curve Theorem
                private bool JordanCurveTheorem(List<Point> vList, int n, float x, float y, ref int CrossNum_X)
                {
                        int x_cross = 0;
                        //int y_cross = 0;
                        float x0, y0, x1, y1;

                        x0 = vList[n - 1].X - x;
                        y0 = vList[n - 1].Y - y;

                        for (int i = 0; i < n; i++)
                        {
                                x1 = vList[i].X - x;
                                y1 = vList[i].Y - y;

                                //測水平
                                if (y0 > 0)
                                {
                                        if (y1 <= 0)
                                                if (x1 * y0 > y1 * x0)
                                                        x_cross++;
                                }
                                else
                                {
                                        if (y1 > 0)
                                                if (x0 * y1 > y0 * x1)
                                                        x_cross++;
                                }

                                ////測垂直
                                //if (x0 > 0)
                                //{
                                //        if (x1 <= 0)
                                //                if (y1 * x0 > x1 * y0)
                                //                        y_cross++;
                                //}
                                //else
                                //{
                                //        if (x1 > 0)
                                //                if (y0 * x1 > x0 * x1)
                                //                        y_cross++;
                                //}
                                x0 = x1;
                                y0 = y1;
                        }

                        CrossNum_X = x_cross;

                        return (x_cross & 1 % 2) == 1;
                }

                private void FillPolygon()
                {
                        for (int w_index = BoundaryL; w_index < BoundaryR; w_index++)
                        {
                                for (int h_index = BoundaryB; h_index < BoundaryT; h_index++)
                                {
                                        int crossNum_x = 0;
                                        bool result = JordanCurveTheorem(vertices, vertices.Count, w_index, h_index, ref crossNum_x);
                                        if (result )
                                                SetQUAD(w_index, h_index, VertexType.InSide);
                                }
                        }
                }

                //八皇后檢查
                private bool CheckRangePoint(Point chk_v ,List<Point> pvertex)
                {
                        //本身不用檢查(最後一個，所以-2)
                        for (int i = 0; i < pvertex.Count - 2; i++)
                        {
                                if (chk_v.X - 1 == pvertex[i].X && chk_v.Y == pvertex[i].Y) return true;
                                if (chk_v.X - 1 == pvertex[i].X && chk_v.Y -1 == pvertex[i].Y) return true;
                                if (chk_v.X - 1 == pvertex[i].X && chk_v.Y +1 == pvertex[i].Y) return true;
                                if (chk_v.X  == pvertex[i].X && chk_v.Y + 1== pvertex[i].Y) return true;
                                if (chk_v.X == pvertex[i].X && chk_v.Y - 1 == pvertex[i].Y) return true;
                                if (chk_v.X +1 == pvertex[i].X && chk_v.Y + 1 == pvertex[i].Y) return true;
                                if (chk_v.X +1 == pvertex[i].X && chk_v.Y  == pvertex[i].Y) return true;
                                if (chk_v.X + 1 == pvertex[i].X && chk_v.Y -1 == pvertex[i].Y) return true;
                        }

                        return false;
                }

                private void openGLControl_MouseDown(object sender, MouseEventArgs e)
                {
                        //滑鼠右鍵
                        if (LockFlag && e.Button == MouseButtons.Right)
                        {
                                rightClicNum++;
                                int crossNum = 0;
                                int X, Y;
                                X = e.X;
                                Y = e.Y;

                                ConvertCoordinate(ref X, ref Y, ConvertType.TypeA2TypeB);

                                MouseRightPoint.X = X;
                                MouseRightPoint.Y = Y;


                                MouseRightMode = true;

                                bool JorandLineFlag = JordanCurveTheorem(vertices, vertices.Count, X, Y, ref crossNum);
                                lbCrossNum.Text = crossNum.ToString();

                                string str = string.Format("{4} => (x,y)=({0},{1} , (X,Y)=({2},{3}) , CrossNum={5}", e.X, e.Y, X, Y, rightClicNum, crossNum);
                                listRightClick.Items.Add(str);
                                listRightClick.SelectedIndex = listRightClick.Items.Count-1;
                        }
                        else
                        {
                                //按左鍵
                                //如果已經封閉，則不允許再畫點
                                if (LockFlag && Mode == CurrentMode.GeneralMode)
                                {
                                        MouseRightMode = false;
                                        MessageBox.Show("多邊形已經封閉，不允許再畫點!");
                                        return;
                                }

                                if (Mode == CurrentMode.GeneralMode)
                                {
                                        clickCount++;
                                        //Point moseLoc = e.Location;

                                        int X, Y;
                                        X = e.X;
                                        Y = e.Y;

                                        ConvertCoordinate(ref X, ref Y, ConvertType.TypeA2TypeB);

                                        //取得 Polygon 的界線
                                        if (clickCount == 1)
                                        {
                                                BoundaryL = X;
                                                BoundaryR = X;
                                                BoundaryT = Y;
                                                BoundaryB = Y;
                                        }
                                        else
                                        {
                                                if (X > BoundaryR) BoundaryR = X;
                                                if (X < BoundaryL) BoundaryL = X;
                                                if (Y > BoundaryT) BoundaryT = Y;
                                                if (Y < BoundaryB) BoundaryB = Y;
                                        }

                                        string str = string.Format("{4} => (x,y)=({0},{1} , (X,Y)=({2},{3}))", e.X, e.Y, X, Y, clickCount);
                                        listResult.Items.Add(str);
                                        listResult.SelectedIndex = listResult.Items.Count - 1;

                                        AddVertex(X, Y);
                                }
                                else
                                {
                                        //EditMode
                                        int X, Y;
                                        X = e.X;
                                        Y = e.Y;

                                        ConvertCoordinate(ref X, ref Y, ConvertType.TypeA2TypeB);

                                        if (choose_index != -1 )
                                        {

                                                vertices.RemoveAt(choose_index);
                                                vertices.Insert(choose_index, choose_point);
                                                choose_index = -1;

                                                //取得 Polygon 的界線

                                                if (X > BoundaryR) BoundaryR = X;
                                                if (X < BoundaryL) BoundaryL = X;
                                                if (Y > BoundaryT) BoundaryT = Y;
                                                if (Y < BoundaryB) BoundaryB = Y;
                                        }
                                        else
                                        {
                                                //挑到哪一個vertex
                                                choose_index = -1;
                                                string str = string.Format("{4} => (x,y)=({0},{1} , (X,Y)=({2},{3}))", e.X, e.Y, X, Y, clickCount);
                                                listResult.Items.Add(str);
                                                listResult.SelectedIndex = listResult.Items.Count - 1;

                                                for (int i = 0; i < vertices.Count ; i++)
                                                {
                                                        if (X == vertices[i].X && Y == vertices[i].Y)
                                                        {
                                                                choose_index = i;
                                                                pre_point = new Point(X, Y);
                                                                choose_point = new Point(X, Y);
                                                                ChoosePointFlag = true;
                                                                break;
                                                        }
                                                }
                                        }


                                }
                        }
                }

                private void SharpGLForm_Load(object sender, EventArgs e)
                {

                }

                //儲存檔案
                private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
                {
                        SaveFileDialog saveDialog = this.saveFileDialog;
                        saveDialog.Filter = "文字檔|*.txt|所有檔|*.*";
                        saveDialog.FileName = "outputfil_" + DateTime.Now.ToString("yyyy_MM_dd");
                        saveDialog.InitialDirectory = Application.StartupPath + ".\\InputData";
                        string outline ="";
                        if (saveDialog.ShowDialog() == DialogResult.OK) // Test result.
                        {
                                 using (StreamWriter sw = new StreamWriter(saveDialog.FileName))  
                                    {
                                            for (int i = 0; i < vertices.Count; i++)
                                            {
                                                    outline = string.Format("{0},{1}",vertices[i].X,vertices[i].Y);
                                                    sw.WriteLine(outline);
                                            }
                                       sw.Flush();  
                                       sw.Close();  
                                    }
                        }
                }

                //開啟檔案
                private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
                {
                        Reset();

                        string fName;
                        string txt_result = "";
                        Stream myStream = null;
                        //int size = -1;

                        this.openFileDialog.Filter = "文字檔|*.txt|所有檔|*.*";
                        this.openFileDialog.InitialDirectory = Application.StartupPath + ".\\InputData";
                        DialogResult result = this.openFileDialog.ShowDialog();
                        if (result == DialogResult.OK) // Test result.
                        {
                                try
                                {
                                        fName = openFileDialog.FileName;
                                        myStream = openFileDialog.OpenFile();
                                        if (myStream != null)
                                        {
                                                StreamReader myReader = new StreamReader(fName);
                                                string line;
                                                line = myReader.ReadLine();
                                                //int count = 0;
                                                while (line != null)
                                                {
                                                        clickCount++;
                                                        int X, Y;
                                                        X = Convert.ToInt32(line.Split(',')[0]);
                                                        Y = Convert.ToInt32(line.Split(',')[1]);

                                                        string str = string.Format("{2} => (X,Y)=({0},{1}))", X, Y, clickCount);
                                                        listResult.Items.Add(str);
                                                        listResult.SelectedIndex = listResult.Items.Count - 1;

                                                        //取得 Polygon 的界線
                                                        if (clickCount == 1)
                                                        {
                                                                BoundaryL = X;
                                                                BoundaryR = X;
                                                                BoundaryT = Y;
                                                                BoundaryB = Y;
                                                        }
                                                        else
                                                        {
                                                                if (X > BoundaryR) BoundaryR = X;
                                                                if (X < BoundaryL) BoundaryL = X;
                                                                if (Y > BoundaryT) BoundaryT = Y;
                                                                if (Y < BoundaryB) BoundaryB = Y;
                                                        }


                                                        Point point = new Point();
                                                        point.X = X;
                                                        point.Y = Y;

                                                        vertices.Add(point);
                                                        txt_result += line;
                                                        line = myReader.ReadLine();
                                                }
                                                myReader.Close();
                                        }
                                        //string text = File.ReadAllText(file);
                                        //size = text.Length;
                                }
                                catch (Exception)
                                {
                                        MessageBox.Show("Open file fail.");
                                }
                                finally
                                {
                                        if (myStream != null)
                                        {
                                                myStream.Close();
                                        }
                                }
                                
                        }
                }

                //說明
                private void helpToolStripMenuItem_Click(object sender, EventArgs e)
                {
                        //System.Diagnostics.Process.Start("iexplore.exe", Application.StartupPath + "about.html");
                       MessageBox.Show ("Jordan Curve");
                }

                private void generalModeToolStripMenuItem_Click(object sender, EventArgs e)
                {
                        this.generalModeToolStripMenuItem.CheckState = CheckState.Checked;
                        this.EditModeToolStripMenuItem.CheckState = CheckState.Unchecked;
                        Mode = CurrentMode.GeneralMode;
                }

                private void EditModeToolStripMenuItem_Click(object sender, EventArgs e)
                {
                        this.generalModeToolStripMenuItem.CheckState = CheckState.Unchecked;
                        this.EditModeToolStripMenuItem.CheckState = CheckState.Checked;
                        Mode = CurrentMode.EditMode;
                }

        }
}
