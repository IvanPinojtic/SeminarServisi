using System;
using System.Collections.Generic;
using System.Linq;

namespace SeminarMVCServis.DAL.Models
{
    public static class Seeder
    {
        public static void SeedInitial(this SeminarContext seminarContext)
        {
            if (!seminarContext.User.Any())
            {
                SeedUsers(seminarContext);

                SeedRestorans(seminarContext);

                SeedSastojci(seminarContext);

                SeedJelo(seminarContext);
            }
        }

        private static void SeedJelo(SeminarContext seminarContext)
        {
            var g1 = seminarContext.Gost.FirstOrDefault(g => g.Ime.Equals("Joža"));
            var g2 = seminarContext.Gost.FirstOrDefault(g => g.Ime.Equals("Stipe"));
            var g3 = seminarContext.Gost.FirstOrDefault(g => g.Ime.Equals("Bila"));

            var g4 = seminarContext.Gost.FirstOrDefault(g => g.Ime.Equals("Ima"));
            var g5 = seminarContext.Gost.FirstOrDefault(g => g.Ime.Equals("Mehmet"));

            var riba = seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Riba"));
            var grah = seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Grah"));
            var juhaPonistra = seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Juha od ponistre"));
            var burek = seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Burek"));
            var juhaMrkva = seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Juha od mrkve"));

            seminarContext.GostJelo.AddRange(new List<GostJelo>
            {
                new GostJelo
                {
                    Gost=g1,
                    Jelo=grah
                },
                new GostJelo
                {
                    Gost=g2,
                    Jelo=grah
                },
                new GostJelo
                {
                    Gost=g2,
                    Jelo=burek
                },
                new GostJelo
                {
                    Gost=g3,
                    Jelo=juhaMrkva
                },
                new GostJelo
                {
                    Gost=g4,
                    Jelo=riba
                },
                new GostJelo
                {
                    Gost=g5,
                    Jelo=juhaPonistra
                }
            });

            seminarContext.SaveChanges();
        }

        private static void SeedRestorans(SeminarContext seminarContext)
        {
            var restoran1 = new Restoran
            {
                Adresa = new Adresa
                {
                    Mjesto = "Zagreb",
                    Ulica = "Ilica",
                    Broj = "23",
                    Drzava = "Hrvatska"
                },
                Naziv = "Kod Bozanića",
                Gosti = new List<Gost>
                {
                    new Gost
                    {
                        Ime="Joža",
                        Prezime="Koža"
                    },
                    new Gost
                    {
                        Ime="Stipe",
                        Prezime="Klipe"
                    },
                    new Gost
                    {
                        Ime="Bila",
                        Prezime="Nebila"
                    }
                },
                Menu = new List<Jelo>
                {
                    new Jelo
                    {
                        Naziv="Juha od mrkve",
                        Cijena=66
                    },
                    new Jelo
                    {
                        Naziv="Grah",
                        Cijena=13
                    },
                    new Jelo
                    {
                        Naziv="Burek",
                        Cijena=7
                    },
                }
            };

            var restoran2 = new Restoran
            {
                Adresa = new Adresa
                {
                    Mjesto = "Split",
                    Ulica = "Zelena",
                    Broj = "14",
                    Drzava = "Hrvatska"
                },
                Naziv = "Ae2",
                Gosti = new List<Gost>
                {
                    new Gost
                    {
                        Ime="Ima",
                        Prezime="Nema"
                    },
                    new Gost
                    {
                        Ime="Mehmet",
                        Prezime="Mujkić"
                    }
                },
                Menu = new List<Jelo>
                {
                    new Jelo
                    {
                        Naziv="Juha od ponistre",
                        Cijena=12
                    },
                    new Jelo
                    {
                        Naziv="Riba",
                        Cijena=500
                    }
                }
            };

            seminarContext.Restoran.Add(restoran1);
            seminarContext.Restoran.Add(restoran2);

            seminarContext.SaveChanges();

        }

        private static void SeedSastojci(SeminarContext seminarContext)
        {
            var riba = seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Riba"));
            var grah = seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Grah"));
            var juhaPonistra = seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Juha od ponistre"));
            var burek= seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Burek"));
            var juhaMrkva= seminarContext.Jelo.FirstOrDefault(j => j.Naziv.Equals("Juha od mrkve"));

            var sol = new Sastojak
            {
                Naziv = "Sol",
                Jedinica = "mg"
            };
            var srdela = new Sastojak
            {
                Naziv = "Srdela",
                Jedinica = "kg"
            };
            var voda = new Sastojak
            {
                Naziv = "Voda",
                Jedinica = "L"
            };
            var ponistra = new Sastojak
            {
                Naziv = "Ponistra",
                Jedinica = "prstohvat"
            };
            var mahune = new Sastojak
            {
                Naziv = "Mahune",
                Jedinica = "komad"
            };
            var sir = new Sastojak
            {
                Naziv = "Sir",
                Jedinica = "kg"
            };
            var mrkva = new Sastojak
            {
                Naziv = "Mrkva",
                Jedinica = "kg"
            };

            /*    seminarContext.Sastojak.Add(sol);
                seminarContext.Sastojak.Add(srdela);
                seminarContext.Sastojak.Add(voda);

                seminarContext.SaveChanges();*/

            seminarContext.SastojakJelo.AddRange(new List<SastojakJelo>
            {
                new SastojakJelo
                {
                    Jelo=riba,
                    Sastojak=sol
                },
                new SastojakJelo
                {
                    Jelo=riba,
                    Sastojak=srdela
                },
                new SastojakJelo
                {
                    Jelo=riba,
                    Sastojak=voda
                }
                ,new SastojakJelo
                {
                    Jelo=grah,
                    Sastojak=sol
                },new SastojakJelo
                {
                    Jelo=grah,
                    Sastojak=mahune
                },
                new SastojakJelo
                {
                    Jelo=grah,
                    Sastojak=voda
                },
                new SastojakJelo
                {
                    Jelo=juhaPonistra,
                    Sastojak=ponistra
                },
                new SastojakJelo
                {
                    Jelo=burek,
                    Sastojak=sir
                },
                new SastojakJelo
                {
                    Jelo=juhaMrkva,
                    Sastojak=mrkva
                },
                new SastojakJelo
                {
                    Jelo=juhaMrkva,
                    Sastojak=voda
                }
            });

            seminarContext.SaveChanges();
        }

        private static void SeedUsers(SeminarContext seminarContext)
        {
            var users = new List<User>
            {
                new User
                {
                    UserName = "user1",
                    UserPIN = 1111
                },
                new User
                {
                    UserName = "user2",
                    UserPIN = 2222
                },
                new User
                {
                    UserName = "user3",
                    UserPIN = 3333
                },
                new User
                {
                    UserName = "user4",
                    UserPIN = 4444
                }
            };

            seminarContext.User.AddRange(users);

            seminarContext.SaveChanges();
        }
    }
}