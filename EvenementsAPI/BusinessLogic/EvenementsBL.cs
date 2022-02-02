using EvenementsAPI.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public class EvenementsBL : IEvenementsBL
    {
        public IEnumerable<Evenement> GetList()
        {
            return Repository.Evenements;
        }
        public Evenement Get(int id)
        {
            return Repository.Evenements.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Participation> GetParticipations(int id)
        {
            return new List<Participation>();
        }

        public Evenement Add(Evenement value)
        {
            ValiderChamps(value);

            value.Id = Repository.IdSequenceCategorie++;
            Repository.Evenements.Add(value);

            return value;
        }
        public Evenement Update(int id, Evenement value)
        {
            ValiderChamps(value);

            var evenement = Repository.Evenements.FirstOrDefault(x => x.Id == id);

            if (evenement == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            //evenement = value.Nom;

            return evenement;
        }
        public void Delete(int id)
        {
            var evenement = Repository.Evenements.FirstOrDefault(x => x.Id == id);

            if (evenement == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element inexistant (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            //var categorieAssociee = Repository.Evenements.Where(_ => _.IdsCategories.Contains(id)).Count() > 0;

            //if (categorieAssociee)
            //{
            //    throw new HttpException
            //    {
            //        Errors = new { Errors = $"Element (id = {id}) ne peut être supprimé, car associé à au moins un événement" },
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            Repository.Evenements.Remove(evenement);
        }

        private void ValiderChamps(Evenement value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            //if (String.IsNullOrEmpty(value.Nom))
            //{
            //    throw new HttpException
            //    {
            //        Errors = new { Errors = "Parametres d'entrée non valides: nom de catégorie inexistant" },
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}
        }
    }
}
