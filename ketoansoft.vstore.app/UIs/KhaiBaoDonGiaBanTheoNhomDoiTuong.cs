﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ketoansoft.app.Class.Data;
using DevExpress.XtraEditors;
using ketoansoft.app.Components.clsVproUtility;
using System.Collections;
using ketoansoft.app.Class.Global;
using ketoansoft.app.Diaglog;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ketoansoft.app
{
    public partial class KhaiBaoDonGiaBanTheoNhomDoiTuong : DevComponents.DotNetBar.Metro.MetroForm
    {
        dbVstoreAppDataContext _db = new dbVstoreAppDataContext(Const.builder.ConnectionString);
        private KTDGBTheoNhomDTRepo _KTDGBTheoNhomDTRepo = new KTDGBTheoNhomDTRepo();
        public Unit _unit = new Unit();
        private List<int> _listUpdate = new List<int>();

        public KhaiBaoDonGiaBanTheoNhomDoiTuong()
        {
            InitializeComponent();
        }

        #region Data
        private void Load_Data()
        {
            try
            {
                _KTDGBTheoNhomDTRepo = new KTDGBTheoNhomDTRepo();
                gridData.DataSource = _KTDGBTheoNhomDTRepo.GetAll();
                //_db = new dbVstoreAppDataContext(Const.builder.ConnectionString);
                //gridData.DataSource = null;
                //gridData.DataSource = _db.KT_DMHHs;
                //gridData.RefreshDataSource();
                //gridView1.RefreshData();
            }
            catch (Exception) { }
        }
        private void Save_Data(bool msg)
        {
            try
            {
                _KTDGBTheoNhomDTRepo = new KTDGBTheoNhomDTRepo();
                int i = 0;
                foreach (int pos in _listUpdate)
                {
                    int id = Utils.CIntDef(gridView1.GetRowCellValue(pos, "ID"), 0);
                    KT_DGBTheoNhomDT obj = _KTDGBTheoNhomDTRepo.GetById(id);
                    if (obj != null)
                    {
                        obj.MA_DM = Utils.CStrDef(gridView1.GetRowCellValue(pos, "STT"), "");
                        obj.MA_TK = Utils.CStrDef(gridView1.GetRowCellValue(pos, "MA_TK"), "");
                        obj.TEN_DM = Utils.CStrDef(gridView1.GetRowCellValue(pos, "TEN_DM"), "");
                        obj.DON_VI = Utils.CStrDef(gridView1.GetRowCellValue(pos, "DON_VI"), "");
                        obj.MA_NHOM = Utils.CStrDef(gridView1.GetRowCellValue(pos, "MA_NHOM"), "");
                        obj.TEN_NHOM = Utils.CStrDef(gridView1.GetRowCellValue(pos, "TEN_NHOM"), "");
                        obj.DANH_DAU = Utils.CStrDef(gridView1.GetRowCellValue(pos, "DANH_DAU"), "");
                        obj.DGBAN1 = Utils.CDblDef(gridView1.GetRowCellValue(pos, "DGBAN1"), 0);
                        obj.DGBAN2 = Utils.CDblDef(gridView1.GetRowCellValue(pos, "DGBAN2"), 0);
                        obj.DGBAN3 = Utils.CDblDef(gridView1.GetRowCellValue(pos, "DGBAN3"), 0);
                        obj.DGBAN4 = Utils.CDblDef(gridView1.GetRowCellValue(pos, "DGBAN4"), 0);
                        obj.DGBAN5 = Utils.CDblDef(gridView1.GetRowCellValue(pos, "DGBAN5"), 0);

                        _KTDGBTheoNhomDTRepo.Update(obj);
                        i++;
                    }
                }
                _listUpdate = new List<int>();
                //if (i > 0 && msg)
                //{
                //    MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Save_Tick()
        {
            try
            {
                _KTDGBTheoNhomDTRepo = new KTDGBTheoNhomDTRepo();
                int id = Utils.CIntDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToString(), 0);
                KT_DGBTheoNhomDT obj = _KTDGBTheoNhomDTRepo.GetById(id);
                if (obj != null)
                {
                    obj.DANH_DAU = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DANH_DAU"), "").Trim() == "T" ? "" : "T";
                    _KTDGBTheoNhomDTRepo.Update(obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Save_Duplicate()
        {
            try
            {                
                if (MessageBox.Show("Bạn có muốn copy dòng này thành dòng mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _KTDGBTheoNhomDTRepo = new KTDGBTheoNhomDTRepo();
                    KT_DGBTheoNhomDT obj = new KT_DGBTheoNhomDT();

                    obj.MA_DM = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "STT"), "");
                    obj.MA_TK = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_TK"), "");
                    obj.TEN_DM = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TEN_DM"), "");
                    obj.DON_VI = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DON_VI"), "");
                    obj.MA_NHOM = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_NHOM"), "");
                    obj.TEN_NHOM = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TEN_NHOM"), "");
                    obj.DANH_DAU = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DANH_DAU"), "");
                    obj.DGBAN1 = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DGBAN1"), 0);
                    obj.DGBAN2 = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DGBAN2"), 0);
                    obj.DGBAN3 = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DGBAN3"), 0);
                    obj.DGBAN4 = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DGBAN4"), 0);
                    obj.DGBAN5 = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DGBAN5"), 0);

                    _KTDGBTheoNhomDTRepo.Create(obj);
                    MessageBox.Show("Đã copy dòng này vào cuối bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Remove_Data()
        {
            try
            {
                //if (_listUpdate.Count > 0)
                //{
                //    MessageBox.Show("Hãy thực hiện lưu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                Save_Data(false);

                int id = Utils.CIntDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID"), 0);
                _KTDGBTheoNhomDTRepo = new KTDGBTheoNhomDTRepo();
                _KTDGBTheoNhomDTRepo.Remove(id);

                //MessageBox.Show("Xóa dòng ID:" + Id + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load_Data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Grid Event
        private void gridData_Load(object sender, EventArgs e)
        {
            Unit.UnitGridview(gridView1);
            Load_Data();

            Load_InfoHeader();
        }
        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            // Sự kiện này để người ta không chuyển qua dòng khác được khi có lỗi xảy ra nè
            // Nó nhận giá trị e.Valid của gridView1_ValidateRow để ứng xử
            // neu e,Valid =True thì nó cho chuyển qua dòng khác hoặc làm tác vụ khác
            // và ngược lại
             e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                bool b = _listUpdate.Exists(a => a == gridView1.FocusedRowHandle);
                if (!b)
                {
                    _listUpdate.Add(gridView1.FocusedRowHandle);
                }
            }));
        }
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            //_unit.Get_ColorTick(gridView1, sender, e);
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Load_InfoRows();
        }
        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                //validation
                //GridColumn sttCol = view.Columns["STT"];
                //GridColumn ma_nhomCol = view.Columns["MA_NHOM"];
                //string stt = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, sttCol), "");
                //string ma_nhom = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, ma_nhomCol), "");
                //if (stt.Trim().Length == 0 || ma_nhom.Trim().Length == 0 )
                //{
                //    e.Valid = false;
                //    if (stt.Trim().Length == 0)
                //        view.SetColumnError(sttCol, "STT không được rổng");
                //    if (ma_nhom.Trim().Length == 0)
                //        view.SetColumnError(ma_nhomCol, "Mã nhóm không được rổng");

                //    return;
                //}

                _KTDGBTheoNhomDTRepo = new KTDGBTheoNhomDTRepo();
                //Kiểm tra đây là dòng dữ liệu mới hay cũ, nếu là mới thì mình insert
                if (view.IsNewItemRow(e.RowHandle))
                {
                    //e.RowHandle trả về giá trị int là thứ tự của dòng hiện tại
                    KT_DGBTheoNhomDT obj = new KT_DGBTheoNhomDT();

                    obj.MA_DM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "STT"), "");
                    obj.MA_TK = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_TK"), "");
                    obj.TEN_DM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "TEN_DM"), "");
                    obj.DON_VI = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "DON_VI"), "");
                    obj.MA_NHOM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_NHOM"), "");
                    obj.TEN_NHOM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "TEN_NHOM"), "");
                    obj.DANH_DAU = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "DANH_DAU"), "");
                    obj.DGBAN1 = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DGBAN1"), 0);
                    obj.DGBAN2 = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DGBAN3"), 0);
                    obj.DGBAN4 = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DGBAN4"), 0);
                    obj.DGBAN5 = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DGBAN5"), 0);

                    _KTDGBTheoNhomDTRepo.Create(obj);

                }
                //Cũ thì update
                else
                {
                    int id = Utils.CIntDef(view.GetRowCellValue(gridView1.FocusedRowHandle, "ID"), 0);
                    KT_DGBTheoNhomDT obj = _KTDGBTheoNhomDTRepo.GetById(id);
                    if (obj != null)
                    {
                        obj.MA_DM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "STT"), "");
                        obj.MA_TK = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_TK"), "");
                        obj.TEN_DM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "TEN_DM"), "");
                        obj.DON_VI = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "DON_VI"), "");
                        obj.MA_NHOM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_NHOM"), "");
                        obj.TEN_NHOM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "TEN_NHOM"), "");
                        obj.DANH_DAU = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "DANH_DAU"), "");
                        obj.DGBAN1 = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DGBAN1"), 0);
                        obj.DGBAN2 = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DGBAN3"), 0);
                        obj.DGBAN4 = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DGBAN4"), 0);
                        obj.DGBAN5 = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DGBAN5"), 0);

                        _KTDGBTheoNhomDTRepo.Update(obj);
                    }

                }
                Load_Data();
            }
            catch (Exception ex)
            {
                e.Valid = false;
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save_Data(true);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có muốn thoát không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    Save_Data(false);
            //    this.Close();
            //}
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            gridView1.ShowFindPanel();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Remove_Data();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            _unit.Get_AllowEdit(gridView1);
        }
        private void btnToCol_Click(object sender, EventArgs e)
        {
            FocusCol();
        }
        private void btnTick_Click(object sender, EventArgs e)
        {
            Save_Tick();
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            Save_Duplicate();
            Load_Data();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

        #region Funtion
        private void FocusCol()
        {
            dlogToCol dialog = new dlogToCol(gridView1);
            dialog.ShowDialog();
            string _value = dialog.Result;
            gridView1.FocusedColumn = gridView1.Columns[_value];
        }
        private void Load_InfoHeader()
        {
            txtMonth.Text = Utils.CStrDef(fTerm._month,"");
            txtYear.Text = Utils.CStrDef(fTerm._year, "");
            txtDatabase.Text = Unit.Get_DatabaseName();
            txtCompanyName.Text = Unit.Get_CompanyName();
            txtNumRows.Text = Utils.CStrDef(gridView1.RowCount, "0") + " of " + Utils.CStrDef(gridView1.RowCount, "0");
        }
        private void Load_InfoRows()
        {
            //string[] _arr = {"SO_KU","SO_HD","MA_TK", "MA_DTPN"};
            txtInfoRows.Text = Unit.Get_InfoRows(gridView1, null);
        }
        #endregion

        #region shortcutKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            KeyEventArgs e = new KeyEventArgs(keyData);
            if (e.KeyCode == Keys.F1)
            {
                btnSearch_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                Save_Tick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                Save_Duplicate();
                Load_Data();
            }
            else if (e.KeyCode == Keys.F5)
            {
                Load_Data();
            }
            else if (e.KeyCode == Keys.F6)
            {
                FocusCol();
            }
            else if (e.KeyCode == Keys.S && e.Control)
            {
                Save_Data(true); return true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(null, null);
            }
            else if (e.KeyCode == Keys.Q && e.Control)
            {
                btnExit_Click(null, null); return true;
            }
            else if (e.KeyCode == Keys.F10)
            {
                _unit.Get_AllowEdit(gridView1);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion        

    }
}