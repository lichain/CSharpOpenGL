namespace CSharpOpenGL
{
        partial class SharpGLForm
        {
                /// <summary>
                /// Required designer variable.
                /// </summary>
                private System.ComponentModel.IContainer components = null;

                /// <summary>
                /// Clean up any resources being used.
                /// </summary>
                /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
                protected override void Dispose(bool disposing)
                {
                        if (disposing && (components != null))
                        {
                                components.Dispose();
                        }
                        base.Dispose(disposing);
                }

                #region Windows Form Designer generated code

                /// <summary>
                /// Required method for Designer support - do not modify
                /// the contents of this method with the code editor.
                /// </summary>
                private void InitializeComponent()
                {
                        this.openGLControl = new SharpGL.OpenGLControl();
                        this.lbMouse = new System.Windows.Forms.Label();
                        this.lbConvert = new System.Windows.Forms.Label();
                        this.listResult = new System.Windows.Forms.ListBox();
                        this.btnDraw = new System.Windows.Forms.Button();
                        this.listRightClick = new System.Windows.Forms.ListBox();
                        this.label1 = new System.Windows.Forms.Label();
                        this.lbCrossNum = new System.Windows.Forms.Label();
                        this.lblClickVertexList = new System.Windows.Forms.Label();
                        this.label2 = new System.Windows.Forms.Label();
                        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
                        this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                        this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                        this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                        this.模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                        this.generalModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                        this.EditModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                        this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                        this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
                        this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                        ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
                        this.menuStrip1.SuspendLayout();
                        this.SuspendLayout();
                        // 
                        // openGLControl
                        // 
                        this.openGLControl.BitDepth = 24;
                        this.openGLControl.Dock = System.Windows.Forms.DockStyle.Left;
                        this.openGLControl.DrawFPS = false;
                        this.openGLControl.FrameRate = 20;
                        this.openGLControl.Location = new System.Drawing.Point(0, 24);
                        this.openGLControl.Name = "openGLControl";
                        this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
                        this.openGLControl.Size = new System.Drawing.Size(555, 555);
                        this.openGLControl.TabIndex = 0;
                        this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
                        this.openGLControl.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLControl_OpenGLDraw);
                        this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
                        this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
                        this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
                        // 
                        // lbMouse
                        // 
                        this.lbMouse.AutoSize = true;
                        this.lbMouse.Location = new System.Drawing.Point(576, 74);
                        this.lbMouse.Name = "lbMouse";
                        this.lbMouse.Size = new System.Drawing.Size(0, 12);
                        this.lbMouse.TabIndex = 1;
                        // 
                        // lbConvert
                        // 
                        this.lbConvert.AutoSize = true;
                        this.lbConvert.Location = new System.Drawing.Point(576, 107);
                        this.lbConvert.Name = "lbConvert";
                        this.lbConvert.Size = new System.Drawing.Size(0, 12);
                        this.lbConvert.TabIndex = 2;
                        // 
                        // listResult
                        // 
                        this.listResult.FormattingEnabled = true;
                        this.listResult.ItemHeight = 12;
                        this.listResult.Location = new System.Drawing.Point(567, 156);
                        this.listResult.Name = "listResult";
                        this.listResult.Size = new System.Drawing.Size(199, 340);
                        this.listResult.TabIndex = 3;
                        // 
                        // btnDraw
                        // 
                        this.btnDraw.Location = new System.Drawing.Point(569, 508);
                        this.btnDraw.Name = "btnDraw";
                        this.btnDraw.Size = new System.Drawing.Size(75, 38);
                        this.btnDraw.TabIndex = 4;
                        this.btnDraw.Text = "Reset";
                        this.btnDraw.UseVisualStyleBackColor = true;
                        this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
                        // 
                        // listRightClick
                        // 
                        this.listRightClick.FormattingEnabled = true;
                        this.listRightClick.ItemHeight = 12;
                        this.listRightClick.Location = new System.Drawing.Point(772, 156);
                        this.listRightClick.Name = "listRightClick";
                        this.listRightClick.Size = new System.Drawing.Size(281, 340);
                        this.listRightClick.TabIndex = 5;
                        // 
                        // label1
                        // 
                        this.label1.AutoSize = true;
                        this.label1.Location = new System.Drawing.Point(700, 74);
                        this.label1.Name = "label1";
                        this.label1.Size = new System.Drawing.Size(66, 12);
                        this.label1.TabIndex = 6;
                        this.label1.Text = "CrossNum：";
                        // 
                        // lbCrossNum
                        // 
                        this.lbCrossNum.AutoSize = true;
                        this.lbCrossNum.Location = new System.Drawing.Point(768, 74);
                        this.lbCrossNum.Name = "lbCrossNum";
                        this.lbCrossNum.Size = new System.Drawing.Size(0, 12);
                        this.lbCrossNum.TabIndex = 7;
                        // 
                        // lblClickVertexList
                        // 
                        this.lblClickVertexList.AutoSize = true;
                        this.lblClickVertexList.Location = new System.Drawing.Point(567, 138);
                        this.lblClickVertexList.Name = "lblClickVertexList";
                        this.lblClickVertexList.Size = new System.Drawing.Size(84, 12);
                        this.lblClickVertexList.TabIndex = 8;
                        this.lblClickVertexList.Text = "Click Vertex List";
                        // 
                        // label2
                        // 
                        this.label2.AutoSize = true;
                        this.label2.Location = new System.Drawing.Point(770, 138);
                        this.label2.Name = "label2";
                        this.label2.Size = new System.Drawing.Size(71, 12);
                        this.label2.TabIndex = 9;
                        this.label2.Text = "Test Point List";
                        // 
                        // menuStrip1
                        // 
                        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.模式ToolStripMenuItem,
            this.helpToolStripMenuItem});
                        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
                        this.menuStrip1.Name = "menuStrip1";
                        this.menuStrip1.Size = new System.Drawing.Size(1079, 24);
                        this.menuStrip1.TabIndex = 10;
                        this.menuStrip1.Text = "menuStrip1";
                        // 
                        // fileToolStripMenuItem
                        // 
                        this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem});
                        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
                        this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
                        this.fileToolStripMenuItem.Text = "File";
                        // 
                        // openFileToolStripMenuItem
                        // 
                        this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
                        this.openFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
                        this.openFileToolStripMenuItem.Text = "Open File";
                        this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
                        // 
                        // saveFileToolStripMenuItem
                        // 
                        this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
                        this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
                        this.saveFileToolStripMenuItem.Text = "Save File";
                        this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
                        // 
                        // 模式ToolStripMenuItem
                        // 
                        this.模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalModeToolStripMenuItem,
            this.EditModeToolStripMenuItem});
                        this.模式ToolStripMenuItem.Name = "模式ToolStripMenuItem";
                        this.模式ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
                        this.模式ToolStripMenuItem.Text = "模式";
                        // 
                        // generalModeToolStripMenuItem
                        // 
                        this.generalModeToolStripMenuItem.Checked = true;
                        this.generalModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
                        this.generalModeToolStripMenuItem.Name = "generalModeToolStripMenuItem";
                        this.generalModeToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
                        this.generalModeToolStripMenuItem.Text = "一般";
                        this.generalModeToolStripMenuItem.Click += new System.EventHandler(this.generalModeToolStripMenuItem_Click);
                        // 
                        // EditModeToolStripMenuItem
                        // 
                        this.EditModeToolStripMenuItem.Name = "EditModeToolStripMenuItem";
                        this.EditModeToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
                        this.EditModeToolStripMenuItem.Text = "編輯";
                        this.EditModeToolStripMenuItem.Click += new System.EventHandler(this.EditModeToolStripMenuItem_Click);
                        // 
                        // helpToolStripMenuItem
                        // 
                        this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
                        this.helpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
                        this.helpToolStripMenuItem.Text = "說明";
                        this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
                        // 
                        // SharpGLForm
                        // 
                        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
                        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                        this.ClientSize = new System.Drawing.Size(1079, 579);
                        this.Controls.Add(this.label2);
                        this.Controls.Add(this.lblClickVertexList);
                        this.Controls.Add(this.lbCrossNum);
                        this.Controls.Add(this.label1);
                        this.Controls.Add(this.listRightClick);
                        this.Controls.Add(this.btnDraw);
                        this.Controls.Add(this.listResult);
                        this.Controls.Add(this.lbConvert);
                        this.Controls.Add(this.lbMouse);
                        this.Controls.Add(this.openGLControl);
                        this.Controls.Add(this.menuStrip1);
                        this.MainMenuStrip = this.menuStrip1;
                        this.MaximizeBox = false;
                        this.Name = "SharpGLForm";
                        this.Text = "SharpGL Form  - Jordan Curve";
                        this.Load += new System.EventHandler(this.SharpGLForm_Load);
                        ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
                        this.menuStrip1.ResumeLayout(false);
                        this.menuStrip1.PerformLayout();
                        this.ResumeLayout(false);
                        this.PerformLayout();

                }

                #endregion

                private SharpGL.OpenGLControl openGLControl;
                private System.Windows.Forms.Label lbMouse;
                private System.Windows.Forms.Label lbConvert;
                private System.Windows.Forms.ListBox listResult;
                private System.Windows.Forms.Button btnDraw;
                private System.Windows.Forms.ListBox listRightClick;
                private System.Windows.Forms.Label label1;
                private System.Windows.Forms.Label lbCrossNum;
                private System.Windows.Forms.Label lblClickVertexList;
                private System.Windows.Forms.Label label2;
                private System.Windows.Forms.MenuStrip menuStrip1;
                private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
                private System.Windows.Forms.OpenFileDialog openFileDialog;
                private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem 模式ToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem generalModeToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem EditModeToolStripMenuItem;
                private System.Windows.Forms.SaveFileDialog saveFileDialog;
        }
}

