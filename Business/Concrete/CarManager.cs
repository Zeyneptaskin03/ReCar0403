﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
            public CarManager(ICarDal carDal)
            {
            _carDal = carDal;
            }

        public IResult Add(Car car)
        {
            if (car.DailyPrice <= 0 && car.Description.Length <= 2)
            {
                return new ErrorResult(Messages.CarAdded);
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
            
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 23) //saati düzelt burda denerken
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);

            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max));
        }

        public IDataResult<Car> GetById(int Id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == Id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int BrandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == BrandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int ColorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == ColorId));

        }

        //IDataResult<Car> ICarService.GetCarsByBrandId(int BrandId)
        //{
        //    throw new NotImplementedException();
        //}

        //IDataResult<Car> ICarService.GetCarsByColorId(int ColorId)
        //{
        //    throw new NotImplementedException();
        //}
        public IResult Update(Car car)
        {

            if (car.Description.Length < 2)
            {
                return new ErrorResult(Messages.CarUpdated);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
           

        }

    }
}