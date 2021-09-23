using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetList() {
            return _context.Pets
                .Include(pet => pet.petOwner);
        }

        [HttpGet("{id}")]
        public Pet GetById(int id) {
            return _context.Pets
                .Include(pet => pet.petOwner)
                .SingleOrDefault(p => p.id == id);
        }

        [HttpPost]
        public Pet Post(Pet pet) {
            _context.Add(pet);
            _context.SaveChanges();

            return pet;
        }

        [HttpPut("{id}")]
        public Pet Put(int id, Pet pet) {

            _context.Pets.Update(pet);
            _context.SaveChanges();

            return pet;
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            Pet pet = _context.Pets.Find(id);
            _context.Pets.Remove(pet);
            _context.SaveChanges();
        }

        [HttpPut("{id}/checkin")]
        public Pet Put(int id)
        {
            // grabbing the pet info from db
            Pet pet = _context.Pets.Find(id);

            // modifying the pet info (not on db yet)
            pet.checkedInAt = DateTime.Now;

            // update the db with new pet info and
            // save changes to db
            _context.Pets.Update(pet);
            _context.SaveChanges();

            return pet;
        }

        [HttpPut("{id}/checkout")]

        // declare 2nd variable to ensure the line below
        // isn't identified somewhere else
        public Pet Put(int id, int a)
        {
            // grabbing the pet info from db
            Pet pet = _context.Pets.Find(id);

            // modifying the pet info (not on db yet)
            pet.checkedInAt = null;

            // update the db with new pet info and
            // save changes to db
            _context.Pets.Update(pet);
            _context.SaveChanges();

            return pet;
        }
        

        // TESTING STUFF
        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }
    }
}
