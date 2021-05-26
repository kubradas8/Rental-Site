using FullCRUDImplementationWithJquery.Models;
using Newtonsoft.Json;
using RentalCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RentalCRUD.Controllers
{
    public class HomeController : Controller
    {
        RentalDBEntities db = new RentalDBEntities();

        public ActionResult Index()
        {
            //search
            var data = db.Advert.ToList();
            ViewBag.result = data;

            List<Advert> adList = db.Advert.ToList();
            List<Category> CatList = db.Category.ToList();
            List<Country> countryList = db.Country.ToList();
            List<State> stateList = db.State.ToList();

            //dropdownlist
            ViewBag.ListOfCategory = new SelectList(CatList, "Id", "CategoryName");
            ViewBag.ListOfCountry = new SelectList(countryList, "Id", "CountryName");

            return View();
        }
        public ActionResult GetStateList(int Country_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<State> StateList = db.State.Where(x => x.CountryId == Country_Id).ToList();
            ViewBag.ListOfState = new SelectList(StateList, "Id", "StateName");
            return Json(StateList, JsonRequestBehavior.AllowGet);

        }

        //get
        public ActionResult GetAdvertList()
        {
            List<Advert> AdList = db.Advert.ToList();
            List<Category> CatList = db.Category.ToList();
            List<Country> countryList = db.Country.ToList();
            List<State> stateList = db.State.ToList();
            List<CarDetails> carList = db.CarDetails.ToList();
            List<HomeDetails> homeList = db.HomeDetails.ToList();
            List<WorkDetails> workList = db.WorkDetails.ToList();
            var advm = from a in AdList
                       join cat in CatList on a.CategoryId equals cat.Id
                       join count in countryList on a.Country_Id equals count.Id
                       join state in stateList on a.State_Id equals state.Id
                       join car in carList on a.CarDetID equals car.Id
                       join home in homeList on a.HomeDetID equals home.Id
                       join work in workList on a.WorkDetID equals work.Id
                       select new AdvertViewModel
                       {
                           Id = a.Id,
                           Title = a.Title,
                           AdvertOwner = a.AdvertOwner,
                           Description = a.Description,
                           Price = a.Price,
                           CountryName = a.Country.CountryName,
                           StateName = a.State.StateName,
                           CategoryName = a.Category.CategoryName,
                           Date = a.Date.ToString("dd/MM/yyyy"),
                           CarDetID = a.CarDetID,
                           HomeDetID = a.HomeDetID,
                           WorkDetID = a.WorkDetID,
                           RoomCount = a.HomeDetails.RoomCount,
                           Age = a.HomeDetails.Age,
                           Floor = a.HomeDetails.Floor,
                           Balcony = a.HomeDetails.Balcony,
                           HeatingSystem = a.HomeDetails.HeatingSystem,
                           Brand = a.CarDetails.Brand,
                           CarModel = a.CarDetails.Model,
                           Gear = a.CarDetails.Gear,
                           ModelYear = a.CarDetails.ModelYear,
                           Color = a.CarDetails.Color,
                           FuelType = a.CarDetails.FuelType,
                           Type = a.WorkDetails.Type,
                           Status = a.WorkDetails.Status,
                           WorkAge = a.WorkDetails.WorkAge,
                           WorkRoom = a.WorkDetails.WorkRoom
                       };

            return Json(advm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAdvertById(int Id)
        {
            Advert model = db.Advert.Where(x => x.Id == Id).FirstOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        //add/edit
        public ActionResult SaveDataInDatabase(AdvertViewModel model)
        {
            var result = false;
            try
            {
                if (model.Id > 0)
                {
                    Advert adv = db.Advert.Where(x => x.Id == model.Id).FirstOrDefault();
                    adv.Title = model.Title;
                    adv.AdvertOwner = model.AdvertOwner;
                    adv.Description = model.Description;
                    adv.Price = model.Price;
                    adv.Country_Id = model.Country_Id;
                    adv.State_Id = model.State_Id;
                    adv.CategoryId = model.CategoryId;
                    adv.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

                    adv.HomeDetails.RoomCount = model.RoomCount;
                    adv.HomeDetails.Age = model.Age;
                    adv.HomeDetails.Floor = model.Floor;
                    adv.HomeDetails.Balcony = model.Balcony;
                    adv.HomeDetails.HeatingSystem = model.HeatingSystem;

                    adv.CarDetails.Brand = model.Brand;
                    adv.CarDetails.Model = model.CarModel;
                    adv.CarDetails.ModelYear = model.ModelYear;
                    adv.CarDetails.Gear = model.Gear;
                    adv.CarDetails.FuelType = model.FuelType;
                    adv.CarDetails.Color = model.Color;

                    adv.WorkDetails.Type = model.Type;
                    adv.WorkDetails.Status = model.Status;
                    adv.WorkDetails.WorkAge = model.WorkAge;
                    adv.WorkDetails.WorkRoom = model.WorkRoom;

                    db.SaveChanges();
                    result = true;
                }
                else
                {

                    CarDetails carDet = new CarDetails();                 
                    carDet.Brand = model.Brand;
                    carDet.Model = model.CarModel;
                    carDet.ModelYear = model.ModelYear;
                    carDet.Color = model.Color;
                    carDet.FuelType = model.FuelType;
                    carDet.Gear = model.Gear;
                    db.CarDetails.Add(carDet);
                    db.SaveChanges();

                    HomeDetails homeDet = new HomeDetails();
                    homeDet.RoomCount = model.RoomCount;
                    homeDet.Floor = model.Floor;
                    homeDet.Age = model.Age;
                    homeDet.Balcony = model.Balcony;
                    homeDet.HeatingSystem = model.HeatingSystem;
                    db.HomeDetails.Add(homeDet);
                    db.SaveChanges();

                    WorkDetails workDet = new WorkDetails();
                    workDet.Type = model.Type;
                    workDet.Status = model.Status;
                    workDet.WorkAge = model.WorkAge;
                    workDet.WorkRoom = model.WorkRoom;
                    db.WorkDetails.Add(workDet);
                    db.SaveChanges();

                    Advert adv = new Advert();
                    adv.Title = model.Title;
                    adv.AdvertOwner = model.AdvertOwner;
                    adv.Description = model.Description;
                    adv.Price = model.Price;
                    adv.Country_Id = model.Country_Id;
                    adv.State_Id = model.State_Id;
                    adv.CategoryId = model.CategoryId;
                    adv.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
                    adv.CarDetID = carDet.Id;
                    adv.HomeDetID = homeDet.Id;
                    adv.WorkDetID = workDet.Id;
                    db.Advert.Add(adv);
                    db.SaveChanges();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //delete
        public ActionResult DeleteAdvertRecord(int Id)
        {
            bool result = false;
            Advert adv = db.Advert.Where(x => x.Id == Id).FirstOrDefault();
            HomeDetails home = db.HomeDetails.Where(x => x.Id == adv.HomeDetID).FirstOrDefault();
            CarDetails car = db.CarDetails.Where(x => x.Id == adv.CarDetID).FirstOrDefault();
            WorkDetails work = db.WorkDetails.Where(x => x.Id == adv.WorkDetID).FirstOrDefault();
            if (adv != null)
            {
                db.HomeDetails.Remove(home);
                db.CarDetails.Remove(car);
                db.WorkDetails.Remove(work);
                db.Advert.Remove(adv);
            
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}