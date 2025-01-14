﻿using TECHNOLOGY_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TECHNOLOGY_SHOP.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: LoaiSanPham
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var allLoai = from loai in data.tb_LoaiSanPhams select loai;
            return View(allLoai);
        }
        public ActionResult Detail(int id)
        {
            var detailLoai = data.tb_LoaiSanPhams.Where(m => m.idLoaiSP == id).First();
            return View(detailLoai);
        }
        public ActionResult Create()
        {
            ViewBag.idHang = new SelectList(data.tb_HangSanPhams, "idHang", "tenHang");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_LoaiSanPham loai)
        {
            bool c_trangThai;
            var c_idHang = collection["idHang"];
            var c_tenLoai = collection["tenLoaiSP"];
            
            if(collection["trangThai"]==null)
                c_trangThai = Convert.ToBoolean(collection["trangThai"]);
            else
                c_trangThai = true;           

            if (string.IsNullOrEmpty(c_tenLoai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                loai.tenLoaiSP = c_tenLoai.ToString();
                loai.trangThai = c_trangThai;
                data.tb_LoaiSanPhams.InsertOnSubmit(loai);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.lstHang = new SelectList(data.tb_HangSanPhams, "idHang", "tenHang");
            var e_loaiSP = data.tb_LoaiSanPhams.First(m => m.idLoaiSP == id);
            return View(e_loaiSP);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            bool e_trangThai;
            var e_loaiSP = data.tb_LoaiSanPhams.First(m => m.idLoaiSP == id);
            var e_idHang = collection["idHang"];
            var e_tenLoai = collection["tenLoaiSP"];
            if (collection["trangThai"] == null)
                e_trangThai = Convert.ToBoolean(collection["trangThai"]);
            else
                e_trangThai = true;
            e_loaiSP.idLoaiSP = id;
            if (string.IsNullOrEmpty(e_tenLoai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                e_loaiSP.tenLoaiSP = e_tenLoai;
                e_loaiSP.trangThai = e_trangThai;
                UpdateModel(e_loaiSP);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var d_loaiSP = data.tb_LoaiSanPhams.First(m => m.idLoaiSP == id);
            return View(d_loaiSP);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var d_loaiSP = data.tb_LoaiSanPhams.Where(m => m.idLoaiSP == id).First();
            data.tb_LoaiSanPhams.DeleteOnSubmit(d_loaiSP);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}