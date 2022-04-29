using BeHeard.Application;
using BeHeard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BeHeard.Application.Models;
using BeHeard.Application.Helpers;
using BeHeard.Core;
using BeHeard.Services;

namespace BeHeard.Controllers
{
    public class AdminController : Controller
    {
        private readonly BeHeardContextManager _beHeardContextManager;
        public AdminController(BeHeardContext context)
        {
            _beHeardContextManager = new BeHeardContextManager(context);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var users = _beHeardContextManager.UserRepository.GetAll();

            var model = new AdminViewModel();
            model.users = _beHeardContextManager.UserRepository.Count();
            model.activities = users.Sum(u => u.Counter);

            return View(model);
        }
        public ActionResult Delete(Guid Id)
        {
            var user = _beHeardContextManager.UserRepository.Get(Id);
            _beHeardContextManager.UserRepository.Remove(user);
            _beHeardContextManager.SaveChanges();

            string redirectLocation = "Index";

            return Redirect(redirectLocation);
        }


        public ActionResult Show(Guid Id)
        {
            var user = _beHeardContextManager.UserRepository.Get(Id);
            var userProfile = _beHeardContextManager.UserProfileRepository.GetUserProfileByUser(user);
            var model = new ProfileViewModel { UserProfile = userProfile };

            return PartialView("_ModalView", model);
        }
        public ActionResult Edit(Guid Id)
        {
            var load = _beHeardContextManager.UserRepository.Get(Id);
            var user = _beHeardContextManager.UserRepository.GetUserByUsername(load.Username);
            var model = new EditViewModel
            {
                Address = user.Address,
                Settings = user.Settings,
                Gender = Enum.GetName(typeof(Gender), user.Gender),
            };
            return PartialView("_UserEditView", model);
        }
        public ActionResult UserTable(string searchField)
        {
            var model = _beHeardContextManager.UserRepository.GetAll();


            if (!String.IsNullOrEmpty(searchField))
            {
                if (searchField == "Specialist")
                {
                    model = model.Where(u => u.Role == (RoleType)2);
                    model = model.ToList();
                }
                else
                {
                    model = model.Where(u => u.LastName.Contains(searchField)
                                   || u.FirstName.Contains(searchField)
                                   || u.Username.Contains(searchField));
                    model = model.ToList();
                }
            }

            string redirectLocation = "_UserTableView";

            return PartialView(redirectLocation, model);
        }

        [HttpPost]
        public ActionResult SaveEdit(EditViewModel model)
        {
            var user = _beHeardContextManager.UserRepository.GetUserByUsername(model.Settings.User.Username);
            
            //  Set User Changes.
            user.FirstName = model.Settings.User.FirstName;
            user.LastName = model.Settings.User.LastName;
            user.PhoneNumber = model.Settings.User.PhoneNumber;
            user.Email = model.Settings.User.Email;
            user.Gender = model.Settings.User.Gender;
            user.Username = model.Settings.User.Username;
            user.Age = model.Settings.User.Age;

            //  Set Address Changes.
            user.Address.Street = model.Settings.User.Address.Street;
            user.Address.City = model.Settings.User.Address.City;
            user.Address.PostalCode = model.Settings.User.Address.PostalCode;
            user.Address.Country = model.Settings.User.Address.Country;
            //user.Address.State = model.Settings.User.Address.State;

            //user.Address = model.Address;
            //user.Profile = _beHeardContextManager.UserProfileRepository.Get(user.Id);
            _beHeardContextManager.UserRepository.Update(user);
            _beHeardContextManager.SaveChanges();
            //var model = _beHeardContextManager.UserRepository.GetAll();

            string redirectLocation = "Index?saveUpdate=" + user.Id.ToString();

            return Redirect(redirectLocation);
        }

        public ActionResult Demographics(string searchField)
        {
            var users = _beHeardContextManager.UserRepository.GetAllUsers();

            //if (!String.IsNullOrEmpty(searchField))
            //{
            //    model = model.Where(u => u.LastName.Contains(searchField)
            //                   || u.FirstName.Contains(searchField) || u.Username.Contains(searchField));
            //    model = model.ToList();
            //}
            //List<State> States = Localization.Los;

            var model = new DemographicsViewModel {
                stateCount = new List<int> {0},
                ageCountMale = new List<int> { 0 },
                ageCountFemale = new List<int> { 0 },

            };

            foreach (State state in model.States)
            {
                model.stateCount.Add(users.Where(c => c.Address.State.Contains(state.Abbreviation)).Count());
            }

            //  Need to change this all to joins.
            model.ageCountMale.Add(users.Where(c => c.Age < 19 & c.Gender == 0).Count());
            model.ageCountMale.Add(users.Where(c => c.Age < 40 & c.Age > 18 & c.Gender == 0).Count());
            model.ageCountMale.Add(users.Where(c => c.Age < 60 & c.Age > 39 & c.Gender == 0).Count());
            model.ageCountMale.Add(users.Where(c => c.Age < 80 & c.Age > 59 & c.Gender == 0).Count());
            model.ageCountMale.Add(users.Where(c => c.Age > 79 & c.Gender == 0).Count());

            model.ageCountFemale.Add(users.Where(c => c.Age < 19 & c.Gender != 0).Count());
            model.ageCountFemale.Add(users.Where(c => c.Age < 40 & c.Age > 18 & c.Gender != 0).Count());
            model.ageCountFemale.Add(users.Where(c => c.Age < 60 & c.Age > 39 & c.Gender != 0).Count());
            model.ageCountFemale.Add(users.Where(c => c.Age < 80 & c.Age > 59 & c.Gender != 0).Count());
            model.ageCountFemale.Add(users.Where(c => c.Age > 79 & c.Gender != 0).Count());


            string redirectLocation = "_DemographicsView";

            return PartialView(redirectLocation, model);
        }

        public ActionResult PopulateDB()
        {
            string[] firstNames = new string[] { "Aaran", "Aaren", "Aarez", "Aarman", "Aaron", "Aaron-James", "Aarron", "Aaryan", "Aaryn", "Aayan", "Aazaan", "Abaan", "Abbas", "Abdallah", "Abdalroof", "Abdihakim", "Abdirahman", "Abdisalam", "Abdul", "Abdul-Aziz", "Abdulbasir", "Abdulkadir", "Abdulkarem", "Abdulkhader", "Abdullah", "Abdul-Majeed", "Abdulmalik", "Abdul-Rehman", "Abdur", "Abdurraheem", "Abdur-Rahman", "Abdur-Rehmaan", "Abel", "Abhinav", "Abhisumant", "Abid", "Abir", "Abraham", "Abu", "Abubakar", "Ace", "Adain", "Adam", "Adam-James", "Addison", "Addisson", "Adegbola", "Adegbolahan", "Aden", "Adenn", "Adie", "Adil", "Aditya", "Adnan", "Adrian", "Adrien", "Aedan", "Aedin", "Aedyn", "Aeron", "Afonso", "Ahmad", "Ahmed", "Ahmed-Aziz", "Ahoua", "Ahtasham", "Aiadan", "Aidan", "Aiden", "Aiden-Jack", "Aiden-Vee", "Aidian", "Aidy", "Ailin", "Aiman", "Ainsley", "Ainslie", "Airen", "Airidas", "Airlie", "AJ", "Ajay", "A-Jay", "Ajayraj", "Akan", "Akram", "Al", "Ala", "Alan", "Alanas", "Alasdair", "Alastair", "Alber", "Albert", "Albie", "Aldred", "Alec", "Aled", "Aleem", "Aleksandar", "Aleksander", "Aleksandr", "Aleksandrs", "Alekzander", "Alessandro", "Alessio", "Alex", "Alexander", "Alexei", "Alexx", "Alexzander", "Alf", "Alfee", "Alfie", "Alfred", "Alfy", "Alhaji", "Al-Hassan", "Ali", "Aliekber", "Alieu", "Alihaider", "Alisdair", "Alishan", "Alistair", "Alistar", "Alister", "Aliyaan", "Allan", "Allan-Laiton", "Allen", "Allesandro", "Allister", "Ally", "Alphonse", "Altyiab", "Alum", "Alvern", "Alvin", "Alyas", "Amaan", "Aman", "Amani", "Ambanimoh", "Ameer", "Amgad", "Ami", "Amin", "Amir", "Ammaar", "Ammar", "Ammer", "Amolpreet", "Amos", "Amrinder", "Amrit", "Amro", "Anay", "Andrea", "Andreas", "Andrei", "Andrejs", "Andrew", "Andy", "Anees", "Anesu", "Angel", "Angelo", "Angus", "Anir", "Anis", "Anish", "Anmolpreet", "Annan", "Anndra", "Anselm", "Anthony", "Anthony-John", "Antoine", "Anton", "Antoni", "Antonio", "Antony", "Antonyo", "Anubhav", "Aodhan", "Aon", "Aonghus", "Apisai", "Arafat", "Aran", "Arandeep", "Arann", "Aray", "Arayan", "Archibald", "Archie", "Arda", "Ardal", "Ardeshir", "Areeb", "Areez", "Aref", "Arfin", "Argyle", "Argyll", "Ari", "Aria", "Arian", "Arihant", "Aristomenis", "Aristotelis", "Arjuna", "Arlo", "Armaan", "Arman", "Armen", "Arnab", "Arnav", "Arnold", "Aron", "Aronas", "Arran", "Arrham", "Arron", "Arryn", "Arsalan", "Artem", "Arthur", "Artur", "Arturo", "Arun", "Arunas", "Arved", "Arya", "Aryan", "Aryankhan", "Aryian", "Aryn", "Asa", "Asfhan", "Ash", "Ashlee-jay", "Ashley", "Ashton", "Ashton-Lloyd", "Ashtyn", "Ashwin", "Asif", "Asim", "Aslam", "Asrar", "Ata", "Atal", "Atapattu", "Ateeq", "Athol", "Athon", "Athos-Carlos", "Atli", "Atom", "Attila", "Aulay", "Aun", "Austen", "Austin", "Avani", "Averon", "Avi", "Avinash", "Avraham", "Awais", "Awwal", "Axel", "Ayaan", "Ayan", "Aydan", "Ayden", "Aydin", "Aydon", "Ayman", "Ayomide", "Ayren", "Ayrton", "Aytug", "Ayub", "Ayyub", "Azaan", "Azedine", "Azeem", "Azim", "Aziz", "Azlan", "Azzam", "Azzedine", "Babatunmise", "Babur", "Bader", "Badr", "Badsha", "Bailee", "Bailey", "Bailie", "Bailley", "Baillie", "Baley", "Balian", "Banan", "Barath", "Barkley", "Barney", "Baron", "Barrie", "Barry", "Bartlomiej", "Bartosz", "Basher", "Basile", "Baxter", "Baye", "Bayley", "Beau", "Beinn", "Bekim", "Believe", "Ben", "Bendeguz", "Benedict", "Benjamin", "Benjamyn", "Benji", "Benn", "Bennett", "Benny", "Benoit", "Bentley", "Berkay", "Bernard", "Bertie", "Bevin", "Bezalel", "Bhaaldeen", "Bharath", "Bilal", "Bill", "Billy", "Binod", "Bjorn", "Blaike", "Blaine", "Blair", "Blaire", "Blake", "Blazej", "Blazey", "Blessing", "Blue", "Blyth", "Bo", "Boab", "Bob", "Bobby", "Bobby-Lee", "Bodhan", "Boedyn", "Bogdan", "Bohbi", "Bony", "Bowen", "Bowie", "Boyd", "Bracken", "Brad", "Bradan", "Braden", "Bradley", "Bradlie", "Bradly", "Brady", "Bradyn", "Braeden", "Braiden", "Brajan", "Brandan", "Branden", "Brandon", "Brandonlee", "Brandon-Lee", "Brandyn", "Brannan", "Brayden", "Braydon", "Braydyn", "Breandan", "Brehme", "Brendan", "Brendon", "Brendyn", "Breogan", "Bret", "Brett", "Briaddon", "Brian", "Brodi", "Brodie", "Brody", "Brogan", "Broghan", "Brooke", "Brooklin", "Brooklyn", "Bruce", "Bruin", "Bruno", "Brunon", "Bryan", "Bryce", "Bryden", "Brydon", "Brydon-Craig", "Bryn", "Brynmor", "Bryson", "Buddy", "Bully", "Burak", "Burhan", "Butali", "Butchi", "Byron", "Cabhan", "Cadan", "Cade", "Caden", "Cadon", "Cadyn", "Caedan", "Caedyn", "Cael", "Caelan", "Caelen", "Caethan", "Cahl", "Cahlum", "Cai", "Caidan", "Caiden", "Caiden-Paul", "Caidyn", "Caie", "Cailaen", "Cailean", "Caileb-John", "Cailin", "Cain", "Caine", "Cairn", "Cal", "Calan", "Calder", "Cale", "Calean", "Caleb", "Calen", "Caley", "Calib", "Calin", "Callahan", "Callan", "Callan-Adam", "Calley", "Callie", "Callin", "Callum", "Callun", "Callyn", "Calum", "Calum-James", "Calvin", "Cambell", "Camerin", "Cameron", "Campbel", "Campbell", "Camron", "Caolain", "Caolan", "Carl", "Carlo", "Carlos", "Carrich", "Carrick", "Carson", "Carter", "Carwyn", "Casey", "Casper", "Cassy", "Cathal", "Cator", "Cavan", "Cayden", "Cayden-Robert", "Cayden-Tiamo", "Ceejay", "Ceilan", "Ceiran", "Ceirin", "Ceiron", "Cejay", "Celik", "Cephas", "Cesar", "Cesare", "Chad", "Chaitanya", "Chang-Ha", "Charles", "Charley", "Charlie", "Charly", "Chase", "Che", "Chester", "Chevy", "Chi", "Chibudom", "Chidera", "Chimsom", "Chin", "Chintu", "Chiqal", "Chiron" };
            string[] lastNames = new string[] { "Seamus", "Sean", "Seane", "Sean-James", "Sean-Paul", "Sean-Ray", "Seb", "Sebastian", "Sebastien", "Selasi", "Seonaidh", "Sephiroth", "Sergei", "Sergio", "Seth", "Sethu", "Seumas", "Shaarvin", "Shadow", "Shae", "Shahmir", "Shai", "Shane", "Shannon", "Sharland", "Sharoz", "Shaughn", "Shaun", "Shaunpaul", "Shaun-Paul", "Shaun-Thomas", "Shaurya", "Shaw", "Shawn", "Shawnpaul", "Shay", "Shayaan", "Shayan", "Shaye", "Shayne", "Shazil", "Shea", "Sheafan", "Sheigh", "Shenuk", "Sher", "Shergo", "Sheriff", "Sherwyn", "Shiloh", "Shiraz", "Shreeram", "Shreyas", "Shyam", "Siddhant", "Siddharth", "Sidharth", "Sidney", "Siergiej", "Silas", "Simon", "Sinai", "Skye", "Sofian", "Sohaib", "Sohail", "Soham", "Sohan", "Sol", "Solomon", "Sonneey", "Sonni", "Sonny", "Sorley", "Soul", "Spencer", "Spondon", "Stanislaw", "Stanley", "Stefan", "Stefano", "Stefin", "Stephen", "Stephenjunior", "Steve", "Steven", "Steven-lee", "Stevie", "Stewart", "Stewarty", "Strachan", "Struan", "Stuart", "Su", "Subhaan", "Sudais", "Suheyb", "Suilven", "Sukhi", "Sukhpal", "Sukhvir", "Sulayman", "Sullivan", "Sultan", "Sung", "Sunny", "Suraj", "Surien", "Sweyn", "Syed", "Sylvain", "Symon", "Szymon", "Tadd", "Taddy", "Tadhg", "Taegan", "Taegen", "Tai", "Tait", "Taiwo", "Talha", "Taliesin", "Talon", "Talorcan", "Tamar", "Tamiem", "Tammam", "Tanay", "Tane", "Tanner", "Tanvir", "Tanzeel", "Taonga", "Tarik", "Tariq-Jay", "Tate", "Taylan", "Taylar", "Tayler", "Taylor", "Taylor-Jay", "Taylor-Lee", "Tayo", "Tayyab", "Tayye", "Tayyib", "Teagan", "Tee", "Teejay", "Tee-jay", "Tegan", "Teighen", "Teiyib", "Te-Jay", "Temba", "Teo", "Teodor", "Teos", "Terry", "Teydren", "Theo", "Theodore", "Thiago", "Thierry", "Thom", "Thomas", "Thomas-Jay", "Thomson", "Thorben", "Thorfinn", "Thrinei", "Thumbiko", "Tiago", "Tian", "Tiarnan", "Tibet", "Tieran", "Tiernan", "Timothy", "Timucin", "Tiree", "Tisloh", "Titi", "Titus", "Tiylar", "TJ", "Tjay", "T-Jay", "Tobey", "Tobi", "Tobias", "Tobie", "Toby", "Todd", "Tokinaga", "Toluwalase", "Tom", "Tomas", "Tomasz", "Tommi-Lee", "Tommy", "Tomson", "Tony", "Torin", "Torquil", "Torran", "Torrin", "Torsten", "Trafford", "Trai", "Travis", "Tre", "Trent", "Trey", "Tristain", "Tristan", "Troy", "Tubagus", "Turki", "Turner", "Ty", "Ty-Alexander", "Tye", "Tyelor", "Tylar", "Tyler", "Tyler-James", "Tyler-Jay", "Tyllor", "Tylor", "Tymom", "Tymon", "Tymoteusz", "Tyra", "Tyree", "Tyrnan", "Tyrone", "Tyson", "Ubaid", "Ubayd", "Uchenna", "Uilleam", "Umair", "Umar", "Umer", "Umut", "Urban", "Uri", "Usman", "Uzair", "Uzayr", "Valen", "Valentin", "Valentino", "Valery", "Valo", "Vasyl", "Vedantsinh", "Veeran", "Victor", "Victory", "Vinay", "Vince", "Vincent", "Vincenzo", "Vinh", "Vinnie", "Vithujan", "Vladimir", "Vladislav", "Vrishin", "Vuyolwethu", "Wabuya", "Wai", "Walid", "Wallace", "Walter", "Waqaas", "Warkhas", "Warren", "Warrick", "Wasif", "Wayde", "Wayne", "Wei", "Wen", "Wesley", "Wesley-Scott", "Wiktor", "Wilkie", "Will", "William", "William-John", "Willum", "Wilson", "Windsor", "Wojciech", "Woyenbrakemi", "Wyatt", "Wylie", "Wynn", "Xabier", "Xander", "Xavier", "Xiao", "Xida", "Xin", "Xue", "Yadgor", "Yago", "Yahya", "Yakup", "Yang", "Yanick", "Yann", "Yannick", "Yaseen", "Yasin", "Yasir" };
            string[] cities = new string[] { "Aberdeen", "Abilene", "Akron", "Albany", "Albuquerque", "Alexandria", "Allentown", "Amarillo", "Anaheim", "Anchorage", "Ann Arbor", "Antioch", "Apple Valley", "Appleton", "Arlington", "Arvada", "Asheville", "Athens", "Atlanta", "Atlantic City", "Augusta", "Aurora", "Austin", "Bakersfield", "Baltimore", "Barnstable", "Baton Rouge", "Beaumont", "Bel Air", "Bellevue", "Berkeley", "Bethlehem", "Billings", "Birmingham", "Bloomington", "Boise", "Boise City", "Bonita Springs", "Boston", "Boulder", "Bradenton", "Bremerton", "Bridgeport", "Brighton", "Brownsville", "Bryan", "Buffalo", "Burbank", "Burlington", "Cambridge", "Canton", "Cape Coral", "Carrollton", "Cary", "Cathedral City", "Cedar Rapids" };
            string tempPassword = "password";
            var States = Localization.Abbreviations();

            int limit = 100;
            int specialist = 0;

            Random random = new Random();
            // int i;
            for (var i = 0; i < limit; i++)
            {
                User user = new User();

                user.Id = Guid.NewGuid();
                user.FirstName = firstNames[random.Next(0, firstNames.Length - 1)];
                user.LastName = lastNames[random.Next(0, lastNames.Length - 1)];
                user.Username = user.FirstName + user.LastName;
                user.Password = tempPassword;
                user.Age = random.Next(18, 101);
                user.Email = user.Username + "@test.com";
                user.Gender = (Gender)random.Next(0, 2);
                user.Counter = random.Next(0,50);
                user.PhoneNumber = "(" + random.Next(100, 999) + ")-" + random.Next(100,999) + "-" + random.Next(1000, 9999);
                user.icon = "face" + random.Next(1, 6) + ".png";

                if (specialist == 2)
                {
                    user.Role = (RoleType)2;
                    specialist = 0;
                }
                specialist++;

                user.Address = new Address();
                user.Address.City = cities[random.Next(1, cities.Length - 1)];
                user.Address.Street = random.Next(100, 4000) + " Street";
                user.Address.State = States[random.Next(1, 49)];
                
                string pass = "";
                if (user.Password != null)
                {
                    pass = PasswordService.hashPassword(user.Password);
                    user.Password = pass;
                }

                var subscription = new Subscription
                {
                    Type = SubscriptionType.Paid,
                };

                var preferences = new Preferences
                {
                    ColorBlindMode = false,
                    DarkMode = false,
                    TextToSpeech = false,
                };


                var settings = new Settings
                {
                    MasterVolume = 100,
                    Preferences = preferences,
                    Subscription = subscription,
                    User = user,
                };

                var activityResults = new List<ActivityResult>();
                var startingDate = new DateTime(2022, 1, 1);
                var range = (DateTime.Today - startingDate).Days;
                var activityResultsCount = random.Next(10, 100);
                for (var activityCount = 0; activityCount < activityResultsCount; activityCount++)
                {
                    ActivityResult exercise = null;
                    Exercise exerciseType = (Exercise)random.Next(0, 4);
                    switch (exerciseType)
                    {
                        case Exercise.Breathing:
                            exercise = new ActivityResult
                            {
                                UserId = user.Id,
                                Exercise = exerciseType,
                                Difficulty = (ActivityLevel) random.Next(0, 4),
                                Date = startingDate.AddDays(random.Next(range))
                            };
                            break;
                        case Exercise.VolumeChasing:
                            exercise = new ActivityResult
                            {
                                UserId = user.Id,
                                Exercise = exerciseType,
                                Date = startingDate.AddDays(random.Next(range)),
                                Difficulty = (ActivityLevel)random.Next(0,4),
                                Syllable = (Syllable)random.Next(0,4),
                                Decibel = $"~{random.Next(50, 70)}"
                            };
                            break;
                        case Exercise.Phrasing:
                            exercise = new ActivityResult
                            {
                                UserId = user.Id,
                                Exercise = exerciseType,
                                Date = startingDate.AddDays(random.Next(range)),
                                Difficulty = (ActivityLevel)random.Next(0,4),
                                SentenceSet = random.Next(0,10),
                                Decibel = $"~{random.Next(50, 70)}"
                            };
                            break;
                        case Exercise.Rote:
                            exercise = new ActivityResult
                            {
                                UserId = user.Id,
                                Exercise = exerciseType,
                                Date = startingDate.AddDays(random.Next(range)),
                                Difficulty = (ActivityLevel)random.Next(0,4),
                                Category = (Category)random.Next(0,6),
                                SentenceSet = random.Next(0,5),
                                Decibel = $"~{random.Next(50, 70)}"
                            };
                            break;
                    }
                    activityResults.Add(exercise);
                }

                var recordingRecords = new List<RecordingRecord>();
                var recordingRecordsCount = random.Next(10, 100);
                var dummyDataService = new DummyDataService();
                for (var recordingCount = 0; recordingCount < recordingRecordsCount; recordingCount++)
                {
                    recordingRecords.Add(new RecordingRecord
                    {
                        Chosen = dummyDataService.getRandomSentence(),
                        Score = random.Next(70,100)
                    });
                }
                
                var profile = new UserProfile
                {
                    Settings = settings,
                    User = user,
                    ActivityResults = activityResults,
                    RecordingRecords = recordingRecords,
                };

                user.Settings = settings;
                user.Profile = profile;
                user.Date = DateTime.Now;

                //if (_userService.IsAnExistingUser(user.Username))
                //{
                //    TempData["Error"] = "Sorry, that 'Username' and 'Password' combination do not match any record.";
                //    return View("~/Views/Login/Registration.cshtml");
                //}
                try
                {
                    _beHeardContextManager.UserRepository.Add(user);
                }
                catch
                {
                    return BadRequest();
                }
            }
            _beHeardContextManager.SaveChanges();


            // var model = _beHeardContextManager.UserRepository.GetAll();
            //
            // foreach(var user in model)
            // {
            //
            //     for (i = 0; i < user.Counter + 1; i++)
            //     {
            //         var activityresult = new ActivityResult();
            //
            //         activityresult.UserId = user.Id;
            //         activityresult.Exercise = (Exercise)random.Next(0, 5);
            //         _beHeardContextManager.ActivityResultRepository.Add(activityresult);
            //
            //     }
            //     _beHeardContextManager.SaveChanges();
            //
            //
            // }


            return Ok();

        }

    }
}
