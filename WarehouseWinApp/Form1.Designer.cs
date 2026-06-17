namespace WarehouseWinApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLoad = new Button();
            dgvMaterials = new DataGridView();
            lblMaterialNo = new Label();
            txtMaterialNo = new TextBox();
            lblMaterialName = new Label();
            lblUnit = new Label();
            txtMaterialName = new TextBox();
            txtUnit = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            dgvStock = new DataGridView();
            btnLoadStock = new Button();
            txtStockQty = new TextBox();
            lblQty = new Label();
            btnSaveStock = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMaterials).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvStock).BeginInit();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(267, 174);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(166, 48);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Tải Danh mục vật tư";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // dgvMaterials
            // 
            dgvMaterials.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaterials.Location = new Point(2, 241);
            dgvMaterials.Name = "dgvMaterials";
            dgvMaterials.RowHeadersWidth = 51;
            dgvMaterials.Size = new Size(797, 208);
            dgvMaterials.TabIndex = 1;
            dgvMaterials.CellContentClick += dgvMaterials_CellContentClick;
            // 
            // lblMaterialNo
            // 
            lblMaterialNo.AutoSize = true;
            lblMaterialNo.Location = new Point(70, 37);
            lblMaterialNo.Name = "lblMaterialNo";
            lblMaterialNo.Size = new Size(75, 20);
            lblMaterialNo.TabIndex = 2;
            lblMaterialNo.Text = "Mã vật tư:";
            // 
            // txtMaterialNo
            // 
            txtMaterialNo.Location = new Point(170, 34);
            txtMaterialNo.Name = "txtMaterialNo";
            txtMaterialNo.PlaceholderText = "Nhập Mã vật tư";
            txtMaterialNo.Size = new Size(372, 27);
            txtMaterialNo.TabIndex = 3;
            // 
            // lblMaterialName
            // 
            lblMaterialName.AutoSize = true;
            lblMaterialName.Location = new Point(70, 83);
            lblMaterialName.Name = "lblMaterialName";
            lblMaterialName.Size = new Size(77, 20);
            lblMaterialName.TabIndex = 4;
            lblMaterialName.Text = "Tên vật tư:";
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Location = new Point(92, 131);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(55, 20);
            lblUnit.TabIndex = 5;
            lblUnit.Text = "Đơn vị:";
            // 
            // txtMaterialName
            // 
            txtMaterialName.Location = new Point(170, 83);
            txtMaterialName.Name = "txtMaterialName";
            txtMaterialName.PlaceholderText = "Nhập Tên vật tư";
            txtMaterialName.Size = new Size(372, 27);
            txtMaterialName.TabIndex = 6;
            // 
            // txtUnit
            // 
            txtUnit.Location = new Point(170, 131);
            txtUnit.Name = "txtUnit";
            txtUnit.PlaceholderText = "Nhập Đơn vị tính (VD: YD, SF, PC, SH,...)";
            txtUnit.Size = new Size(372, 27);
            txtUnit.TabIndex = 7;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(622, 23);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(166, 48);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(622, 77);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(166, 48);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(622, 131);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(166, 48);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // dgvStock
            // 
            dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStock.Location = new Point(805, 241);
            dgvStock.Name = "dgvStock";
            dgvStock.RowHeadersWidth = 51;
            dgvStock.Size = new Size(562, 208);
            dgvStock.TabIndex = 11;
            dgvStock.CellClick += dgvStock_CellClick;
            // 
            // btnLoadStock
            // 
            btnLoadStock.Location = new Point(1175, 155);
            btnLoadStock.Name = "btnLoadStock";
            btnLoadStock.Size = new Size(138, 47);
            btnLoadStock.TabIndex = 12;
            btnLoadStock.Text = "Xem Tồn Kho";
            btnLoadStock.UseVisualStyleBackColor = true;
            btnLoadStock.Click += btnLoadStock_Click;
            // 
            // txtStockQty
            // 
            txtStockQty.Location = new Point(995, 76);
            txtStockQty.Name = "txtStockQty";
            txtStockQty.PlaceholderText = "Nhập số lượng";
            txtStockQty.Size = new Size(372, 27);
            txtStockQty.TabIndex = 13;
            // 
            // lblQty
            // 
            lblQty.AutoSize = true;
            lblQty.Location = new Point(889, 79);
            lblQty.Name = "lblQty";
            lblQty.Size = new Size(72, 20);
            lblQty.TabIndex = 14;
            lblQty.Text = "Số lượng:";
            // 
            // btnSaveStock
            // 
            btnSaveStock.Location = new Point(995, 155);
            btnSaveStock.Name = "btnSaveStock";
            btnSaveStock.Size = new Size(138, 47);
            btnSaveStock.TabIndex = 15;
            btnSaveStock.Text = "Lưu Tồn Kho";
            btnSaveStock.UseVisualStyleBackColor = true;
            btnSaveStock.Click += btnSaveStock_ClickAsync;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1379, 450);
            Controls.Add(btnSaveStock);
            Controls.Add(lblQty);
            Controls.Add(txtStockQty);
            Controls.Add(btnLoadStock);
            Controls.Add(dgvStock);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtUnit);
            Controls.Add(txtMaterialName);
            Controls.Add(lblUnit);
            Controls.Add(lblMaterialName);
            Controls.Add(txtMaterialNo);
            Controls.Add(lblMaterialNo);
            Controls.Add(dgvMaterials);
            Controls.Add(btnLoad);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvMaterials).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvStock).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLoad;
        private DataGridView dgvMaterials;
        private Label lblMaterialNo;
        private TextBox txtMaterialNo;
        private Label lblMaterialName;
        private Label lblUnit;
        private TextBox txtMaterialName;
        private TextBox txtUnit;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private DataGridView dgvStock;
        private Button btnLoadStock;
        private TextBox txtStockQty;
        private Label lblQty;
        private Button btnSaveStock;
    }
}
