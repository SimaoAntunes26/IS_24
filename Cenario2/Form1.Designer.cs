namespace Cenario2
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.appListBox = new System.Windows.Forms.ListBox();
            this.getAllAppsButton = new System.Windows.Forms.Button();
            this.getAppContainersButton = new System.Windows.Forms.Button();
            this.getAppRecordsButton = new System.Windows.Forms.Button();
            this.getAppNotifsButton = new System.Windows.Forms.Button();
            this.getAppSelectedButton = new System.Windows.Forms.Button();
            this.appIdLabel = new System.Windows.Forms.Label();
            this.appNameLabel = new System.Windows.Forms.Label();
            this.appCreationDateLabel = new System.Windows.Forms.Label();
            this.createAppButton = new System.Windows.Forms.Button();
            this.deleteAppButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.updateAppNameTexbox = new System.Windows.Forms.TextBox();
            this.updateAppButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.createAppNameTextbox = new System.Windows.Forms.TextBox();
            this.createContainerNameTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.updateContainerButton = new System.Windows.Forms.Button();
            this.updateContainerNameTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.deleteContainerButton = new System.Windows.Forms.Button();
            this.createContainerButton = new System.Windows.Forms.Button();
            this.containerCreationDateLabel = new System.Windows.Forms.Label();
            this.containerNameLabel = new System.Windows.Forms.Label();
            this.containerIdLabel = new System.Windows.Forms.Label();
            this.getContainerSelectedButton = new System.Windows.Forms.Button();
            this.getContainerNotifsButton = new System.Windows.Forms.Button();
            this.getContainerRecordsButton = new System.Windows.Forms.Button();
            this.containerListBox = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.createRecordNameTextbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.deleteRecordButton = new System.Windows.Forms.Button();
            this.createRecordButton = new System.Windows.Forms.Button();
            this.recordCreationDateLabel = new System.Windows.Forms.Label();
            this.recordNameLabel = new System.Windows.Forms.Label();
            this.recordIdLabel = new System.Windows.Forms.Label();
            this.getRecordSelectedButton = new System.Windows.Forms.Button();
            this.recordListBox = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.createNotifNameTextbox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.deleteNotifButton = new System.Windows.Forms.Button();
            this.createNotifButton = new System.Windows.Forms.Button();
            this.notifCreationDateLabel = new System.Windows.Forms.Label();
            this.notifNameLabel = new System.Windows.Forms.Label();
            this.notifIdLabel = new System.Windows.Forms.Label();
            this.getNotifSelectedButton = new System.Windows.Forms.Button();
            this.notifListBox = new System.Windows.Forms.ListBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.recordContentTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.createRecordContentTextbox = new System.Windows.Forms.TextBox();
            this.notifEventLabel = new System.Windows.Forms.Label();
            this.notifEndpointLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.createNotifEventCreationRadio = new System.Windows.Forms.RadioButton();
            this.createNotifEventBothRadio = new System.Windows.Forms.RadioButton();
            this.createNotifEventDeletionRadio = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.createNotifEndpointTextbox = new System.Windows.Forms.TextBox();
            this.createNotifRadioGroup = new System.Windows.Forms.GroupBox();
            this.getAllContainersButton = new System.Windows.Forms.Button();
            this.getAllRecordsButton = new System.Windows.Forms.Button();
            this.getAllNotificationsButton = new System.Windows.Forms.Button();
            this.createNotifRadioGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Applications:";
            // 
            // appListBox
            // 
            this.appListBox.FormattingEnabled = true;
            this.appListBox.Location = new System.Drawing.Point(432, 40);
            this.appListBox.Name = "appListBox";
            this.appListBox.Size = new System.Drawing.Size(263, 160);
            this.appListBox.TabIndex = 1;
            // 
            // getAllAppsButton
            // 
            this.getAllAppsButton.Location = new System.Drawing.Point(16, 40);
            this.getAllAppsButton.Name = "getAllAppsButton";
            this.getAllAppsButton.Size = new System.Drawing.Size(88, 22);
            this.getAllAppsButton.TabIndex = 2;
            this.getAllAppsButton.Text = "Get all";
            this.getAllAppsButton.UseVisualStyleBackColor = true;
            this.getAllAppsButton.Click += new System.EventHandler(this.getAllAppsButton_Click);
            // 
            // getAppContainersButton
            // 
            this.getAppContainersButton.Location = new System.Drawing.Point(16, 68);
            this.getAppContainersButton.Name = "getAppContainersButton";
            this.getAppContainersButton.Size = new System.Drawing.Size(88, 22);
            this.getAppContainersButton.TabIndex = 3;
            this.getAppContainersButton.Text = "Get containers";
            this.getAppContainersButton.UseVisualStyleBackColor = true;
            this.getAppContainersButton.Click += new System.EventHandler(this.getAppContainersButton_Click);
            // 
            // getAppRecordsButton
            // 
            this.getAppRecordsButton.Location = new System.Drawing.Point(110, 67);
            this.getAppRecordsButton.Name = "getAppRecordsButton";
            this.getAppRecordsButton.Size = new System.Drawing.Size(88, 22);
            this.getAppRecordsButton.TabIndex = 4;
            this.getAppRecordsButton.Text = "Get records";
            this.getAppRecordsButton.UseVisualStyleBackColor = true;
            this.getAppRecordsButton.Click += new System.EventHandler(this.getAppRecordsButton_Click);
            // 
            // getAppNotifsButton
            // 
            this.getAppNotifsButton.Location = new System.Drawing.Point(16, 96);
            this.getAppNotifsButton.Name = "getAppNotifsButton";
            this.getAppNotifsButton.Size = new System.Drawing.Size(182, 22);
            this.getAppNotifsButton.TabIndex = 5;
            this.getAppNotifsButton.Text = "Get notifications";
            this.getAppNotifsButton.UseVisualStyleBackColor = true;
            this.getAppNotifsButton.Click += new System.EventHandler(this.getAppNotifsButton_Click);
            // 
            // getAppSelectedButton
            // 
            this.getAppSelectedButton.Location = new System.Drawing.Point(110, 40);
            this.getAppSelectedButton.Name = "getAppSelectedButton";
            this.getAppSelectedButton.Size = new System.Drawing.Size(88, 22);
            this.getAppSelectedButton.TabIndex = 6;
            this.getAppSelectedButton.Text = "Get selected";
            this.getAppSelectedButton.UseVisualStyleBackColor = true;
            this.getAppSelectedButton.Click += new System.EventHandler(this.getAppSelectedButton_Click);
            // 
            // appIdLabel
            // 
            this.appIdLabel.AutoSize = true;
            this.appIdLabel.Location = new System.Drawing.Point(204, 45);
            this.appIdLabel.Name = "appIdLabel";
            this.appIdLabel.Size = new System.Drawing.Size(19, 13);
            this.appIdLabel.TabIndex = 7;
            this.appIdLabel.Text = "Id:";
            // 
            // appNameLabel
            // 
            this.appNameLabel.AutoSize = true;
            this.appNameLabel.Location = new System.Drawing.Point(204, 73);
            this.appNameLabel.Name = "appNameLabel";
            this.appNameLabel.Size = new System.Drawing.Size(38, 13);
            this.appNameLabel.TabIndex = 8;
            this.appNameLabel.Text = "Name:";
            // 
            // appCreationDateLabel
            // 
            this.appCreationDateLabel.AutoSize = true;
            this.appCreationDateLabel.Location = new System.Drawing.Point(204, 101);
            this.appCreationDateLabel.Name = "appCreationDateLabel";
            this.appCreationDateLabel.Size = new System.Drawing.Size(33, 13);
            this.appCreationDateLabel.TabIndex = 9;
            this.appCreationDateLabel.Text = "Date:";
            // 
            // createAppButton
            // 
            this.createAppButton.Location = new System.Drawing.Point(16, 124);
            this.createAppButton.Name = "createAppButton";
            this.createAppButton.Size = new System.Drawing.Size(88, 22);
            this.createAppButton.TabIndex = 10;
            this.createAppButton.Text = "Create";
            this.createAppButton.UseVisualStyleBackColor = true;
            this.createAppButton.Click += new System.EventHandler(this.createAppButton_Click);
            // 
            // deleteAppButton
            // 
            this.deleteAppButton.Location = new System.Drawing.Point(16, 180);
            this.deleteAppButton.Name = "deleteAppButton";
            this.deleteAppButton.Size = new System.Drawing.Size(88, 22);
            this.deleteAppButton.TabIndex = 11;
            this.deleteAppButton.Text = "Delete";
            this.deleteAppButton.UseVisualStyleBackColor = true;
            this.deleteAppButton.Click += new System.EventHandler(this.deleteAppButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "New name:";
            // 
            // updateAppNameTexbox
            // 
            this.updateAppNameTexbox.Location = new System.Drawing.Point(177, 152);
            this.updateAppNameTexbox.Name = "updateAppNameTexbox";
            this.updateAppNameTexbox.Size = new System.Drawing.Size(131, 20);
            this.updateAppNameTexbox.TabIndex = 13;
            // 
            // updateAppButton
            // 
            this.updateAppButton.Location = new System.Drawing.Point(16, 152);
            this.updateAppButton.Name = "updateAppButton";
            this.updateAppButton.Size = new System.Drawing.Size(88, 22);
            this.updateAppButton.TabIndex = 14;
            this.updateAppButton.Text = "Update";
            this.updateAppButton.UseVisualStyleBackColor = true;
            this.updateAppButton.Click += new System.EventHandler(this.updateAppButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Name:";
            // 
            // createAppNameTextbox
            // 
            this.createAppNameTextbox.Location = new System.Drawing.Point(177, 124);
            this.createAppNameTextbox.Name = "createAppNameTextbox";
            this.createAppNameTextbox.Size = new System.Drawing.Size(131, 20);
            this.createAppNameTextbox.TabIndex = 16;
            // 
            // createContainerNameTextbox
            // 
            this.createContainerNameTextbox.Location = new System.Drawing.Point(177, 316);
            this.createContainerNameTextbox.Name = "createContainerNameTextbox";
            this.createContainerNameTextbox.Size = new System.Drawing.Size(131, 20);
            this.createContainerNameTextbox.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Name:";
            // 
            // updateContainerButton
            // 
            this.updateContainerButton.Location = new System.Drawing.Point(16, 344);
            this.updateContainerButton.Name = "updateContainerButton";
            this.updateContainerButton.Size = new System.Drawing.Size(88, 22);
            this.updateContainerButton.TabIndex = 31;
            this.updateContainerButton.Text = "Update";
            this.updateContainerButton.UseVisualStyleBackColor = true;
            this.updateContainerButton.Click += new System.EventHandler(this.updateContainerButton_Click);
            // 
            // updateContainerNameTextbox
            // 
            this.updateContainerNameTextbox.Location = new System.Drawing.Point(177, 344);
            this.updateContainerNameTextbox.Name = "updateContainerNameTextbox";
            this.updateContainerNameTextbox.Size = new System.Drawing.Size(131, 20);
            this.updateContainerNameTextbox.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 349);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "New name:";
            // 
            // deleteContainerButton
            // 
            this.deleteContainerButton.Location = new System.Drawing.Point(16, 372);
            this.deleteContainerButton.Name = "deleteContainerButton";
            this.deleteContainerButton.Size = new System.Drawing.Size(88, 22);
            this.deleteContainerButton.TabIndex = 28;
            this.deleteContainerButton.Text = "Delete";
            this.deleteContainerButton.UseVisualStyleBackColor = true;
            this.deleteContainerButton.Click += new System.EventHandler(this.deleteContainerButton_Click);
            // 
            // createContainerButton
            // 
            this.createContainerButton.Location = new System.Drawing.Point(16, 316);
            this.createContainerButton.Name = "createContainerButton";
            this.createContainerButton.Size = new System.Drawing.Size(88, 22);
            this.createContainerButton.TabIndex = 27;
            this.createContainerButton.Text = "Create";
            this.createContainerButton.UseVisualStyleBackColor = true;
            this.createContainerButton.Click += new System.EventHandler(this.createContainerButton_Click);
            // 
            // containerCreationDateLabel
            // 
            this.containerCreationDateLabel.AutoSize = true;
            this.containerCreationDateLabel.Location = new System.Drawing.Point(204, 293);
            this.containerCreationDateLabel.Name = "containerCreationDateLabel";
            this.containerCreationDateLabel.Size = new System.Drawing.Size(33, 13);
            this.containerCreationDateLabel.TabIndex = 26;
            this.containerCreationDateLabel.Text = "Date:";
            // 
            // containerNameLabel
            // 
            this.containerNameLabel.AutoSize = true;
            this.containerNameLabel.Location = new System.Drawing.Point(204, 264);
            this.containerNameLabel.Name = "containerNameLabel";
            this.containerNameLabel.Size = new System.Drawing.Size(38, 13);
            this.containerNameLabel.TabIndex = 25;
            this.containerNameLabel.Text = "Name:";
            // 
            // containerIdLabel
            // 
            this.containerIdLabel.AutoSize = true;
            this.containerIdLabel.Location = new System.Drawing.Point(204, 237);
            this.containerIdLabel.Name = "containerIdLabel";
            this.containerIdLabel.Size = new System.Drawing.Size(19, 13);
            this.containerIdLabel.TabIndex = 24;
            this.containerIdLabel.Text = "Id:";
            // 
            // getContainerSelectedButton
            // 
            this.getContainerSelectedButton.Location = new System.Drawing.Point(110, 232);
            this.getContainerSelectedButton.Name = "getContainerSelectedButton";
            this.getContainerSelectedButton.Size = new System.Drawing.Size(88, 22);
            this.getContainerSelectedButton.TabIndex = 23;
            this.getContainerSelectedButton.Text = "Get selected";
            this.getContainerSelectedButton.UseVisualStyleBackColor = true;
            this.getContainerSelectedButton.Click += new System.EventHandler(this.getContainerSelectedButton_Click);
            // 
            // getContainerNotifsButton
            // 
            this.getContainerNotifsButton.Location = new System.Drawing.Point(16, 288);
            this.getContainerNotifsButton.Name = "getContainerNotifsButton";
            this.getContainerNotifsButton.Size = new System.Drawing.Size(182, 22);
            this.getContainerNotifsButton.TabIndex = 22;
            this.getContainerNotifsButton.Text = "Get notifications";
            this.getContainerNotifsButton.UseVisualStyleBackColor = true;
            this.getContainerNotifsButton.Click += new System.EventHandler(this.getContainerNotifsButton_Click);
            // 
            // getContainerRecordsButton
            // 
            this.getContainerRecordsButton.Location = new System.Drawing.Point(16, 259);
            this.getContainerRecordsButton.Name = "getContainerRecordsButton";
            this.getContainerRecordsButton.Size = new System.Drawing.Size(182, 22);
            this.getContainerRecordsButton.TabIndex = 21;
            this.getContainerRecordsButton.Text = "Get records";
            this.getContainerRecordsButton.UseVisualStyleBackColor = true;
            this.getContainerRecordsButton.Click += new System.EventHandler(this.getContainerRecordsButton_Click);
            // 
            // containerListBox
            // 
            this.containerListBox.FormattingEnabled = true;
            this.containerListBox.Location = new System.Drawing.Point(432, 232);
            this.containerListBox.Name = "containerListBox";
            this.containerListBox.Size = new System.Drawing.Size(263, 160);
            this.containerListBox.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Containers:";
            // 
            // createRecordNameTextbox
            // 
            this.createRecordNameTextbox.Location = new System.Drawing.Point(176, 508);
            this.createRecordNameTextbox.Name = "createRecordNameTextbox";
            this.createRecordNameTextbox.Size = new System.Drawing.Size(66, 20);
            this.createRecordNameTextbox.TabIndex = 50;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(109, 513);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 49;
            this.label10.Text = "Name:";
            // 
            // deleteRecordButton
            // 
            this.deleteRecordButton.Location = new System.Drawing.Point(16, 536);
            this.deleteRecordButton.Name = "deleteRecordButton";
            this.deleteRecordButton.Size = new System.Drawing.Size(88, 22);
            this.deleteRecordButton.TabIndex = 45;
            this.deleteRecordButton.Text = "Delete";
            this.deleteRecordButton.UseVisualStyleBackColor = true;
            this.deleteRecordButton.Click += new System.EventHandler(this.deleteRecordButton_Click);
            // 
            // createRecordButton
            // 
            this.createRecordButton.Location = new System.Drawing.Point(15, 508);
            this.createRecordButton.Name = "createRecordButton";
            this.createRecordButton.Size = new System.Drawing.Size(88, 22);
            this.createRecordButton.TabIndex = 44;
            this.createRecordButton.Text = "Create";
            this.createRecordButton.UseVisualStyleBackColor = true;
            this.createRecordButton.Click += new System.EventHandler(this.createRecordButton_Click);
            // 
            // recordCreationDateLabel
            // 
            this.recordCreationDateLabel.AutoSize = true;
            this.recordCreationDateLabel.Location = new System.Drawing.Point(203, 474);
            this.recordCreationDateLabel.Name = "recordCreationDateLabel";
            this.recordCreationDateLabel.Size = new System.Drawing.Size(33, 13);
            this.recordCreationDateLabel.TabIndex = 43;
            this.recordCreationDateLabel.Text = "Date:";
            // 
            // recordNameLabel
            // 
            this.recordNameLabel.AutoSize = true;
            this.recordNameLabel.Location = new System.Drawing.Point(203, 451);
            this.recordNameLabel.Name = "recordNameLabel";
            this.recordNameLabel.Size = new System.Drawing.Size(38, 13);
            this.recordNameLabel.TabIndex = 42;
            this.recordNameLabel.Text = "Name:";
            // 
            // recordIdLabel
            // 
            this.recordIdLabel.AutoSize = true;
            this.recordIdLabel.Location = new System.Drawing.Point(203, 429);
            this.recordIdLabel.Name = "recordIdLabel";
            this.recordIdLabel.Size = new System.Drawing.Size(19, 13);
            this.recordIdLabel.TabIndex = 41;
            this.recordIdLabel.Text = "Id:";
            // 
            // getRecordSelectedButton
            // 
            this.getRecordSelectedButton.Location = new System.Drawing.Point(110, 424);
            this.getRecordSelectedButton.Name = "getRecordSelectedButton";
            this.getRecordSelectedButton.Size = new System.Drawing.Size(87, 22);
            this.getRecordSelectedButton.TabIndex = 40;
            this.getRecordSelectedButton.Text = "Get selected";
            this.getRecordSelectedButton.UseVisualStyleBackColor = true;
            this.getRecordSelectedButton.Click += new System.EventHandler(this.getRecordSelectedButton_Click);
            // 
            // recordListBox
            // 
            this.recordListBox.FormattingEnabled = true;
            this.recordListBox.Location = new System.Drawing.Point(431, 424);
            this.recordListBox.Name = "recordListBox";
            this.recordListBox.Size = new System.Drawing.Size(263, 160);
            this.recordListBox.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 397);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 34;
            this.label15.Text = "Records:";
            // 
            // createNotifNameTextbox
            // 
            this.createNotifNameTextbox.Location = new System.Drawing.Point(177, 702);
            this.createNotifNameTextbox.Name = "createNotifNameTextbox";
            this.createNotifNameTextbox.Size = new System.Drawing.Size(131, 20);
            this.createNotifNameTextbox.TabIndex = 67;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(109, 705);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 13);
            this.label16.TabIndex = 66;
            this.label16.Text = "Name:";
            // 
            // deleteNotifButton
            // 
            this.deleteNotifButton.Location = new System.Drawing.Point(16, 770);
            this.deleteNotifButton.Name = "deleteNotifButton";
            this.deleteNotifButton.Size = new System.Drawing.Size(88, 22);
            this.deleteNotifButton.TabIndex = 62;
            this.deleteNotifButton.Text = "Delete";
            this.deleteNotifButton.UseVisualStyleBackColor = true;
            this.deleteNotifButton.Click += new System.EventHandler(this.deleteNotifButton_Click);
            // 
            // createNotifButton
            // 
            this.createNotifButton.Location = new System.Drawing.Point(15, 700);
            this.createNotifButton.Name = "createNotifButton";
            this.createNotifButton.Size = new System.Drawing.Size(88, 22);
            this.createNotifButton.TabIndex = 61;
            this.createNotifButton.Text = "Create";
            this.createNotifButton.UseVisualStyleBackColor = true;
            this.createNotifButton.Click += new System.EventHandler(this.createNotifButton_Click);
            // 
            // notifCreationDateLabel
            // 
            this.notifCreationDateLabel.AutoSize = true;
            this.notifCreationDateLabel.Location = new System.Drawing.Point(203, 666);
            this.notifCreationDateLabel.Name = "notifCreationDateLabel";
            this.notifCreationDateLabel.Size = new System.Drawing.Size(33, 13);
            this.notifCreationDateLabel.TabIndex = 60;
            this.notifCreationDateLabel.Text = "Date:";
            // 
            // notifNameLabel
            // 
            this.notifNameLabel.AutoSize = true;
            this.notifNameLabel.Location = new System.Drawing.Point(203, 643);
            this.notifNameLabel.Name = "notifNameLabel";
            this.notifNameLabel.Size = new System.Drawing.Size(38, 13);
            this.notifNameLabel.TabIndex = 59;
            this.notifNameLabel.Text = "Name:";
            // 
            // notifIdLabel
            // 
            this.notifIdLabel.AutoSize = true;
            this.notifIdLabel.Location = new System.Drawing.Point(203, 621);
            this.notifIdLabel.Name = "notifIdLabel";
            this.notifIdLabel.Size = new System.Drawing.Size(19, 13);
            this.notifIdLabel.TabIndex = 58;
            this.notifIdLabel.Text = "Id:";
            // 
            // getNotifSelectedButton
            // 
            this.getNotifSelectedButton.Location = new System.Drawing.Point(110, 616);
            this.getNotifSelectedButton.Name = "getNotifSelectedButton";
            this.getNotifSelectedButton.Size = new System.Drawing.Size(87, 22);
            this.getNotifSelectedButton.TabIndex = 57;
            this.getNotifSelectedButton.Text = "Get selected";
            this.getNotifSelectedButton.UseVisualStyleBackColor = true;
            this.getNotifSelectedButton.Click += new System.EventHandler(this.getNotifSelectedButton_Click);
            // 
            // notifListBox
            // 
            this.notifListBox.FormattingEnabled = true;
            this.notifListBox.Location = new System.Drawing.Point(431, 616);
            this.notifListBox.Name = "notifListBox";
            this.notifListBox.Size = new System.Drawing.Size(263, 251);
            this.notifListBox.TabIndex = 52;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(12, 589);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(82, 13);
            this.label21.TabIndex = 51;
            this.label21.Text = "Notifications:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 449);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 68;
            this.label6.Text = "Content:";
            // 
            // recordContentTextbox
            // 
            this.recordContentTextbox.Location = new System.Drawing.Point(16, 471);
            this.recordContentTextbox.Name = "recordContentTextbox";
            this.recordContentTextbox.ReadOnly = true;
            this.recordContentTextbox.Size = new System.Drawing.Size(181, 20);
            this.recordContentTextbox.TabIndex = 69;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(248, 513);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "Content:";
            // 
            // createRecordContentTextbox
            // 
            this.createRecordContentTextbox.Location = new System.Drawing.Point(323, 508);
            this.createRecordContentTextbox.Name = "createRecordContentTextbox";
            this.createRecordContentTextbox.Size = new System.Drawing.Size(66, 20);
            this.createRecordContentTextbox.TabIndex = 71;
            // 
            // notifEventLabel
            // 
            this.notifEventLabel.AutoSize = true;
            this.notifEventLabel.Location = new System.Drawing.Point(13, 643);
            this.notifEventLabel.Name = "notifEventLabel";
            this.notifEventLabel.Size = new System.Drawing.Size(38, 13);
            this.notifEventLabel.TabIndex = 74;
            this.notifEventLabel.Text = "Event:";
            // 
            // notifEndpointLabel
            // 
            this.notifEndpointLabel.AutoSize = true;
            this.notifEndpointLabel.Location = new System.Drawing.Point(12, 666);
            this.notifEndpointLabel.Name = "notifEndpointLabel";
            this.notifEndpointLabel.Size = new System.Drawing.Size(52, 13);
            this.notifEndpointLabel.TabIndex = 75;
            this.notifEndpointLabel.Text = "Endpoint:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(110, 729);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 76;
            this.label12.Text = "Event:";
            // 
            // createNotifEventCreationRadio
            // 
            this.createNotifEventCreationRadio.AutoSize = true;
            this.createNotifEventCreationRadio.Location = new System.Drawing.Point(6, 1);
            this.createNotifEventCreationRadio.Name = "createNotifEventCreationRadio";
            this.createNotifEventCreationRadio.Size = new System.Drawing.Size(64, 17);
            this.createNotifEventCreationRadio.TabIndex = 77;
            this.createNotifEventCreationRadio.TabStop = true;
            this.createNotifEventCreationRadio.Text = "Creation";
            this.createNotifEventCreationRadio.UseVisualStyleBackColor = true;
            // 
            // createNotifEventBothRadio
            // 
            this.createNotifEventBothRadio.AutoSize = true;
            this.createNotifEventBothRadio.Location = new System.Drawing.Point(146, 0);
            this.createNotifEventBothRadio.Name = "createNotifEventBothRadio";
            this.createNotifEventBothRadio.Size = new System.Drawing.Size(47, 17);
            this.createNotifEventBothRadio.TabIndex = 78;
            this.createNotifEventBothRadio.TabStop = true;
            this.createNotifEventBothRadio.Text = "Both";
            this.createNotifEventBothRadio.UseVisualStyleBackColor = true;
            // 
            // createNotifEventDeletionRadio
            // 
            this.createNotifEventDeletionRadio.AutoSize = true;
            this.createNotifEventDeletionRadio.Location = new System.Drawing.Point(76, 1);
            this.createNotifEventDeletionRadio.Name = "createNotifEventDeletionRadio";
            this.createNotifEventDeletionRadio.Size = new System.Drawing.Size(64, 17);
            this.createNotifEventDeletionRadio.TabIndex = 79;
            this.createNotifEventDeletionRadio.TabStop = true;
            this.createNotifEventDeletionRadio.Text = "Deletion";
            this.createNotifEventDeletionRadio.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(109, 754);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 80;
            this.label13.Text = "Endpoint:";
            // 
            // createNotifEndpointTextbox
            // 
            this.createNotifEndpointTextbox.Location = new System.Drawing.Point(177, 751);
            this.createNotifEndpointTextbox.Name = "createNotifEndpointTextbox";
            this.createNotifEndpointTextbox.Size = new System.Drawing.Size(131, 20);
            this.createNotifEndpointTextbox.TabIndex = 81;
            // 
            // createNotifRadioGroup
            // 
            this.createNotifRadioGroup.Controls.Add(this.createNotifEventCreationRadio);
            this.createNotifRadioGroup.Controls.Add(this.createNotifEventDeletionRadio);
            this.createNotifRadioGroup.Controls.Add(this.createNotifEventBothRadio);
            this.createNotifRadioGroup.Location = new System.Drawing.Point(174, 727);
            this.createNotifRadioGroup.Name = "createNotifRadioGroup";
            this.createNotifRadioGroup.Size = new System.Drawing.Size(214, 16);
            this.createNotifRadioGroup.TabIndex = 88;
            this.createNotifRadioGroup.TabStop = false;
            // 
            // getAllContainersButton
            // 
            this.getAllContainersButton.Location = new System.Drawing.Point(16, 231);
            this.getAllContainersButton.Name = "getAllContainersButton";
            this.getAllContainersButton.Size = new System.Drawing.Size(88, 22);
            this.getAllContainersButton.TabIndex = 89;
            this.getAllContainersButton.Text = "Get all";
            this.getAllContainersButton.UseVisualStyleBackColor = true;
            this.getAllContainersButton.Click += new System.EventHandler(this.getAllContainersButton_Click);
            // 
            // getAllRecordsButton
            // 
            this.getAllRecordsButton.Location = new System.Drawing.Point(16, 424);
            this.getAllRecordsButton.Name = "getAllRecordsButton";
            this.getAllRecordsButton.Size = new System.Drawing.Size(88, 22);
            this.getAllRecordsButton.TabIndex = 90;
            this.getAllRecordsButton.Text = "Get all";
            this.getAllRecordsButton.UseVisualStyleBackColor = true;
            this.getAllRecordsButton.Click += new System.EventHandler(this.getAllRecordsButton_Click);
            // 
            // getAllNotificationsButton
            // 
            this.getAllNotificationsButton.Location = new System.Drawing.Point(16, 616);
            this.getAllNotificationsButton.Name = "getAllNotificationsButton";
            this.getAllNotificationsButton.Size = new System.Drawing.Size(88, 22);
            this.getAllNotificationsButton.TabIndex = 91;
            this.getAllNotificationsButton.Text = "Get all";
            this.getAllNotificationsButton.UseVisualStyleBackColor = true;
            this.getAllNotificationsButton.Click += new System.EventHandler(this.getAllNotificationsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 883);
            this.Controls.Add(this.getAllNotificationsButton);
            this.Controls.Add(this.getAllRecordsButton);
            this.Controls.Add(this.getAllContainersButton);
            this.Controls.Add(this.createNotifRadioGroup);
            this.Controls.Add(this.createNotifEndpointTextbox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.notifEndpointLabel);
            this.Controls.Add(this.notifEventLabel);
            this.Controls.Add(this.createRecordContentTextbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.recordContentTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.createNotifNameTextbox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.deleteNotifButton);
            this.Controls.Add(this.createNotifButton);
            this.Controls.Add(this.notifCreationDateLabel);
            this.Controls.Add(this.notifNameLabel);
            this.Controls.Add(this.notifIdLabel);
            this.Controls.Add(this.getNotifSelectedButton);
            this.Controls.Add(this.notifListBox);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.createRecordNameTextbox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.deleteRecordButton);
            this.Controls.Add(this.createRecordButton);
            this.Controls.Add(this.recordCreationDateLabel);
            this.Controls.Add(this.recordNameLabel);
            this.Controls.Add(this.recordIdLabel);
            this.Controls.Add(this.getRecordSelectedButton);
            this.Controls.Add(this.recordListBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.createContainerNameTextbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.updateContainerButton);
            this.Controls.Add(this.updateContainerNameTextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.deleteContainerButton);
            this.Controls.Add(this.createContainerButton);
            this.Controls.Add(this.containerCreationDateLabel);
            this.Controls.Add(this.containerNameLabel);
            this.Controls.Add(this.containerIdLabel);
            this.Controls.Add(this.getContainerSelectedButton);
            this.Controls.Add(this.getContainerNotifsButton);
            this.Controls.Add(this.getContainerRecordsButton);
            this.Controls.Add(this.containerListBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.createAppNameTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.updateAppButton);
            this.Controls.Add(this.updateAppNameTexbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.deleteAppButton);
            this.Controls.Add(this.createAppButton);
            this.Controls.Add(this.appCreationDateLabel);
            this.Controls.Add(this.appNameLabel);
            this.Controls.Add(this.appIdLabel);
            this.Controls.Add(this.getAppSelectedButton);
            this.Controls.Add(this.getAppNotifsButton);
            this.Controls.Add(this.getAppRecordsButton);
            this.Controls.Add(this.getAppContainersButton);
            this.Controls.Add(this.getAllAppsButton);
            this.Controls.Add(this.appListBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.createNotifRadioGroup.ResumeLayout(false);
            this.createNotifRadioGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox appListBox;
        private System.Windows.Forms.Button getAllAppsButton;
        private System.Windows.Forms.Button getAppContainersButton;
        private System.Windows.Forms.Button getAppRecordsButton;
        private System.Windows.Forms.Button getAppNotifsButton;
        private System.Windows.Forms.Button getAppSelectedButton;
        private System.Windows.Forms.Label appIdLabel;
        private System.Windows.Forms.Label appNameLabel;
        private System.Windows.Forms.Label appCreationDateLabel;
        private System.Windows.Forms.Button createAppButton;
        private System.Windows.Forms.Button deleteAppButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox updateAppNameTexbox;
        private System.Windows.Forms.Button updateAppButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox createAppNameTextbox;
        private System.Windows.Forms.TextBox createContainerNameTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button updateContainerButton;
        private System.Windows.Forms.TextBox updateContainerNameTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button deleteContainerButton;
        private System.Windows.Forms.Button createContainerButton;
        private System.Windows.Forms.Label containerCreationDateLabel;
        private System.Windows.Forms.Label containerNameLabel;
        private System.Windows.Forms.Label containerIdLabel;
        private System.Windows.Forms.Button getContainerSelectedButton;
        private System.Windows.Forms.Button getContainerNotifsButton;
        private System.Windows.Forms.Button getContainerRecordsButton;
        private System.Windows.Forms.ListBox containerListBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox createRecordNameTextbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button deleteRecordButton;
        private System.Windows.Forms.Button createRecordButton;
        private System.Windows.Forms.Label recordCreationDateLabel;
        private System.Windows.Forms.Label recordNameLabel;
        private System.Windows.Forms.Label recordIdLabel;
        private System.Windows.Forms.Button getRecordSelectedButton;
        private System.Windows.Forms.ListBox recordListBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox createNotifNameTextbox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button deleteNotifButton;
        private System.Windows.Forms.Button createNotifButton;
        private System.Windows.Forms.Label notifCreationDateLabel;
        private System.Windows.Forms.Label notifNameLabel;
        private System.Windows.Forms.Label notifIdLabel;
        private System.Windows.Forms.Button getNotifSelectedButton;
        private System.Windows.Forms.ListBox notifListBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox recordContentTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox createRecordContentTextbox;
        private System.Windows.Forms.Label notifEventLabel;
        private System.Windows.Forms.Label notifEndpointLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton createNotifEventCreationRadio;
        private System.Windows.Forms.RadioButton createNotifEventBothRadio;
        private System.Windows.Forms.RadioButton createNotifEventDeletionRadio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox createNotifEndpointTextbox;
        private System.Windows.Forms.GroupBox createNotifRadioGroup;
        private System.Windows.Forms.Button getAllContainersButton;
        private System.Windows.Forms.Button getAllRecordsButton;
        private System.Windows.Forms.Button getAllNotificationsButton;
    }
}

