using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RudesWebapp.Models;

namespace RudesWebapp.Data
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public static class RudesDatabaseSeeder
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            Initialize(serviceScope);
        }

        public static void Initialize(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<RudesDatabaseContext>();
            //context.Database.EnsureCreated(); // TODO not sure if needed
            //context.Database.Migrate(); // TODO not sure if needed

            // var signInManager = serviceScope.ServiceProvider.GetService<SignInManager<User>>();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

            if (context.Article != null && context.Article.Any())
                return; // database already seeded

            using (var transaction = context.Database.BeginTransaction())
            {
                var images = GetImages();
                context.Image.AddRange(images);
                context.SaveChanges();

                var articles = GetArticles(context).ToArray();
                context.Article.AddRange(articles);
                context.SaveChanges();

                var discounts = GetDiscounts(context);
                context.Discount.AddRange(discounts);
                context.SaveChanges();

                foreach (var aa in GetArticleAvailabilities(context))
                {
                    context.ArticleAvailability.Add(aa);
                }

                context.SaveChanges();

                var posts = GetPosts(context);
                context.Post.AddRange(posts);
                context.SaveChanges();

                var players = GetPlayers(context);
                context.Player.AddRange(players);
                context.SaveChanges();

                var matches = GetMatches();
                context.Match.AddRange(matches);
                context.SaveChanges();

                CreateUsers(GetUsers(), userManager);
                SetupUserRoles(serviceScope.ServiceProvider).Wait();
                context.SaveChanges();

                var shoppingCartArticles = GetShoppingCartArticles(context);
                context.ShoppingCartArticle.AddRange(shoppingCartArticles);
                context.SaveChanges();

                var reviews = GetReviews(context);
                context.Review.AddRange(reviews);
                context.SaveChanges();

                var orders = GetOrders(context);
                context.Order.AddRange(orders);
                context.SaveChanges();

                var orderArticles = GetOrderArticles(context);
                context.OrderArticle.AddRange(orderArticles);
                context.SaveChanges();
                context.SaveChangesAsync();

                transaction.Commit();
            }
        }

        private static IEnumerable<Image> GetImages()
        {
            var images = new List<Image>()
            {
                new Image
                {
                    Name = "dummy1.png",
                    OriginalName = "dummyimage.png",
                    AltText = "Dummy image alt text",
                    Caption = "Dummy image caption"
                },
                new Image
                {
                    Name = "dummy2.png",
                    OriginalName = "dummyimage.png",
                    AltText = "Dummy image alt text",
                    Caption = "Dummy image caption"
                },
                new Image
                {
                    Name = "dummy3.png",
                    OriginalName = "dummyimage.png",
                    AltText = "Dummy image alt text",
                    Caption = "Dummy image caption"
                },
                new Image
                {
                    Name = "dummy4.png",
                    OriginalName = "dummyimage.png",
                    AltText = "Dummy image alt text",
                    Caption = "Dummy image caption"
                }
            };
            return images;
        }

        private static List<Article> GetArticles(RudesDatabaseContext context)
        {
            var articles = new List<Article>()
            {
                new Article
                {
                    Name = "Special Christmas Hoodie",
                    Description =
                        "Super special red hoodie with Rudes logo. Rudes Hoodie izrađena je od kvalitetne 320 gr felpe. Topla i ugodna, idealan je izbor za sve sportaše kao i za navijače. Veliki prednji džep te vezice na ovratniku. ",
                    Image = context.Image.First(),
                    Price = 250,
                    Type = "hoodie",
                    ArticleColor = Color.Indigo
                },
                new Article
                {
                    Name = "Anniversary T-shirt",
                    Description =
                        "Rare anniversary T-shirt. Machine wash cold with like colors, dry low heat. Solid colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.Skip(1).First(),
                    Price = 200,
                    Type = "t-shirt",
                    ArticleColor = Color.Indigo
                },
                new Article
                {
                    Name = "T-shirt",
                    Description =
                        "Rudes majica izrađena je od 100% pamuka s tiskanim motivom na prednjoj strani. Pravi izbor za sve navijače i simpatizere maksimirskog kluba.",
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Indigo
                },
                new Article
                {
                    Name = "Majica 2",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Aquamarine
                },
                new Article
                {
                    Name = "Majica 3",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Aquamarine
                },
                new Article
                {
                    Name = "Stone long sleeve tshirt",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "hoodie",
                    ArticleColor = Color.Aquamarine
                },
                new Article
                {
                    Name = "Majica 4",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Hudica ",
                    Description =
                        "Rudes hoodie izrađena je od 100% pamuka s tiskanim motivom na prednjoj strani. Pravi izbor za sve navijače i simpatizere maksimirskog kluba.",
                    Price = 120,
                    Type = "hoodie",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Hudica 2",
                    Description =
                        "Rare anniversary T-shirt. Machine wash cold with like colors, dry low heat. 33 colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.Skip(1).First(),
                    Price = 200,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Hudica 3",
                    Description =
                        "Rare anniversary hoodie. Machine wash cold with like colors, dry low heat. 11 colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.First(),
                    Price = 200,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Hudica 4",
                    Description =
                        "Rare anniversary hoodie. 11 wash cold with like colors, dry low heat. Solid colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.First(),
                    Price = 200,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Majica 5",
                    Description =
                        "Rare anniversary hoodie. Machine wash cold 11 like colors, dry low heat. Solid colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.First(),
                    Price = 200,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Anniversary majica ",
                    Description =
                        "Rare anniversary hoodie. Machine wash cold hehe like colors, dry low heat. Solid colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.First(),
                    Price = 200,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Anniversary hudica",
                    Description =
                        "Rare anniversary hoodie. Machine hehe cold with like colors, dry low heat. Solid colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.Skip(1).First(),
                    Price = 200,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Majica 7",
                    Description =
                        "Rare anniversary hoodie. Machine wash bla with like colors, dry low heat. Solid colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.First(),
                    Price = 200,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Majica 8",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Majica 9",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Hudica 9",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Hudica 11",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Crimson
                },
                new Article
                {
                    Name = "Hudica 12",
                    Description =
                        "Vivamus aliquam erat ac velit tristique pretium. Nulla consequat orci odio, quis lacinia orci ultricies a.",
                    Image = context.Image.Skip(1).First(),
                    Price = 120,
                    Type = "t-shirt",
                    ArticleColor = Color.Gold
                }
            };
            return articles;
        }

        private static IEnumerable<Discount> GetDiscounts(RudesDatabaseContext context)
        {
            var discounts = new List<Discount>()
            {
                new Discount
                {
                    Article = context.Article.First(),
                    Percentage = 30,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(14)
                },
                new Discount
                {
                    Article = context.Article.Skip(1).First(),
                    Percentage = 15,
                    StartDate = DateTime.Now.AddDays(7),
                    EndDate = DateTime.Now.AddDays(21)
                }
            };

            return discounts;
        }

        private static IEnumerable<ArticleAvailability> GetArticleAvailabilities(RudesDatabaseContext context)
        {
            var articleAvailabilities = new List<ArticleAvailability>
            {
                new ArticleAvailability
                {
                    Article = context.Article.OrderByDescending(e => e.Id).First(),
                    Size = "M",
                    Quantity = 10
                },
                new ArticleAvailability
                {
                    Article = context.Article.OrderByDescending(e => e.Id).First(),
                    Size = "L",
                    Quantity = 5
                }
            };
            return articleAvailabilities;
        }

        private static IEnumerable<Post> GetPosts(RudesDatabaseContext context)
        {
            var posts = new List<Post>
            {
                new Post
                {
                    Title = "Tijesan poraz Rudeša od Jazina",
                    Content =
                        "Rudeš je u završnici ispustio mogućnost da skine prvi ”skalp” vodećim Jazinama. U KC Dražen Petrović momčad iz Zadra ostala je neporažena i nakon ovog kola slavivši pobjedu 90:84. Susret je rješen u posljednje dvije minute. Rudeš je posljednji puta vodio 83:81 na ulasku u posljednje tri minute. Arian Došen je s linije slobodnih bacanja izjednačio, Filip Torić promašio oba bacanja, a David Ušić odveo goste do prednosti. Posljednju nadu Rudešu ugasilo je promašeno bacanje i polaganje Marka Juriča. Stipe Krstanović (21) je predvodio Jazine Arbanase. Ušić i Došen su dodali po 16, a Rudešu nije pomogla odlična utakmica Tomislava Cvitkovića (28 poena). Hrvoje Garafolić je dodao 13.",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(14),
                    Image = context.Image.Skip(2).First(),
                    PostType = "Novost"
                },
                new Post
                {
                    Title = "Lorem ipsum",
                    Content =
                        "<h2>What is Lorem Ipsum?</h2> <p><strong>Lorem Ipsum</strong> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMinutes(30),
                    Image = context.Image.Skip(2).First(),
                    PostType = "Novost"
                },
                new Post
                {
                    Title = "Where can I get some?",
                    Content =
                        "<p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p>",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    Image = context.Image.Skip(2).First(),
                    PostType = "Novost"
                },
                new Post
                {
                    Title = "Prvi post",
                    Content =
                        "<div id= \"Content \"> <div id= \"bannerL \"><div id= \"div-gpt-ad-1474537762122-2 \"> <script type= \"text/javascript \">googletag.cmd.push(function() { googletag.display( \"div-gpt-ad-1474537762122-2 \"); });</script> </div></div> <div id= \"bannerR \"><div id= \"div-gpt-ad-1474537762122-3 \"> <script type= \"text/javascript \">googletag.cmd.push(function() { googletag.display( \"div-gpt-ad-1474537762122-3 \"); });</script> </div></div> <div class= \"boxed \"><!--    If you want to use Lorem Ipsum within another program please contact us for details on our API rather than parse the HTML below, we have XML and JSON available.    --><div id= \"lipsum \"> <p> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque sed nunc pharetra, suscipit lorem sollicitudin, lobortis lorem. Suspendisse consequat eu ante nec rhoncus. Vivamus bibendum tortor in dui vehicula ultrices. Sed auctor tempus finibus. Integer lectus elit, gravida scelerisque lacus vitae, laoreet mattis erat. Phasellus ut mauris ante. Mauris et quam eu velit blandit finibus. Fusce vel turpis vel tellus vehicula facilisis. Cras consectetur est at mauris blandit pellentesque. Nullam cursus faucibus finibus. Nam nunc erat, aliquam sit amet velit non, fermentum cursus lorem. Duis sollicitudin molestie nunc. Fusce dolor lacus, iaculis vitae libero at, sagittis condimentum purus. Duis euismod ante quis urna venenatis venenatis. Integer feugiat, nisl ut vestibulum facilisis, nibh urna rutrum ligula, vel sollicitudin tortor orci eu mi. Quisque in arcu sed arcu iaculis sodales. </p> <p> Suspendisse sagittis consectetur lacus ac molestie. Donec egestas rhoncus ligula, a sodales mi placerat sed. Aliquam vulputate consequat turpis non dignissim. Donec sodales nec dolor eu aliquam. Morbi accumsan, lacus nec gravida pretium, tellus magna pretium est, non interdum orci eros eu diam. Sed ex justo, dapibus in condimentum ac, suscipit sit amet metus. Aliquam sit amet varius orci. Maecenas scelerisque urna non magna volutpat ultricies. Ut eget justo ante. Nullam feugiat pharetra mattis. Vestibulum eget tincidunt felis. Etiam eget est vitae sem congue bibendum nec eget dolor. In vulputate risus vel mi eleifend lobortis. Morbi suscipit dolor vitae luctus tristique. Vestibulum porta tempor odio, a rutrum magna blandit id. </p> <p> Phasellus vitae faucibus magna. Nunc posuere rutrum turpis eget auctor. Duis consectetur interdum est, at vulputate dolor rhoncus vel. Nulla id ornare quam. In fringilla, dolor ac scelerisque auctor, felis erat rutrum arcu, commodo maximus libero ante nec elit. Duis convallis diam vel dolor rhoncus, ac scelerisque dolor facilisis. Ut id sagittis magna, id faucibus sapien. Proin quis lacus eu magna condimentum facilisis sit amet eget justo. Aliquam quis accumsan nisi. Nam non lectus ultricies, euismod diam eget, pharetra justo. Integer ultrices posuere dictum. Integer feugiat imperdiet scelerisque. Nam vestibulum et nulla in maximus. Donec tempus tellus eget nunc lobortis, vel volutpat est vestibulum. Proin venenatis neque et urna luctus venenatis. </p> <p> Quisque lobortis mi sem, et molestie nisi mollis vel. Phasellus iaculis odio ac lacus fermentum aliquam vitae tempus massa. Maecenas eget ullamcorper eros. Suspendisse efficitur eleifend eros, ac hendrerit massa facilisis sed. Phasellus aliquet ligula volutpat dictum dignissim. Vivamus ut lacinia libero, nec pellentesque tortor. Etiam at aliquet erat. Aliquam placerat cursus molestie. Proin sit amet turpis eleifend quam condimentum sollicitudin et id ex. Integer at maximus diam. Phasellus faucibus ex a condimentum congue. Nullam a lectus at metus pharetra gravida at non est. Nam eu orci diam. Vivamus sit amet elit id odio porta vulputate. </p> <p> Suspendisse ultricies felis dictum venenatis finibus. Pellentesque interdum erat vel libero condimentum hendrerit. In id mi ligula. Morbi nec tortor imperdiet, dignissim metus et, bibendum tortor. Vivamus non auctor odio, sit amet porta nulla. Curabitur at nisl dolor. Sed tempus in est non egestas. Maecenas eu tincidunt ante. Suspendisse porttitor tellus ut est pretium, a fringilla orci hendrerit. Aliquam aliquam sed diam ut feugiat. Vivamus eu dapibus ex. Pellentesque tincidunt pellentesque elit, non convallis enim pellentesque id. Nullam eu mollis enim. Nullam purus lorem, tristique eu feugiat et, pulvinar convallis dolor. Praesent pulvinar viverra tempus. Aliquam a neque sapien. </p></div> <div id= \"generated \">Generated 5 paragraphs, 544 words, 3728 bytes of <a href= \"https://www.lipsum.com/ \" title= \"Lorem Ipsum \">Lorem Ipsum</a></div> </div> </div>",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.MaxValue,
                    Image = context.Image.Skip(2).First(),
                    PostType = "Obavijest"
                },
                new Post
                {
                    Title = "Sretan Božić svima",
                    Content =
                        "KK Rudeš želi vam svima sretan Božić i Novu godinu!",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.MaxValue,
                    Image = context.Image.Skip(2).First(),
                    PostType = "Obavijest"
                },
                new Post
                {
                    Title = "Humanitarna akcija",
                    Content =
                        "KK Rudeš organizira dobrotvornu utakmicu na kojoj se prikupljaju donacije za nezbrinutu djecu. Ovime vas sve pozivamo da dođete i podržite humanitarnu akciju.",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.MaxValue,
                    Image = context.Image.Skip(2).First(),
                    PostType = "Obavijest"
                },
                new Post
                {
                    Title = "Finale",
                    Content =
                        "Ovogodišnje finale između KK Rudeš i KK Dubrava odigrat će se u subotu, 4.1.2020.",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.MaxValue,
                    Image = context.Image.Skip(2).First(),
                    PostType = "Obavijest"
                },
                new Post
                {
                    Title = "Upisi",
                    Content =
                        "Podsjećamo da se upisati u KK Rudeš možete svakim danom od 9 do 17 sati. ",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.MaxValue,
                    Image = context.Image.Skip(2).First(),
                    PostType = "Obavijest"
                },
                new Post
                {
                    Title = "Dodjela medalja",
                    Content =
                        "Dodjela medalja ovogodišnjim igračima godine održat će se 15.1.2020.",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.MaxValue,
                    Image = context.Image.Skip(2).First(),
                    PostType = "Obavijest"
                },
                new Post
                {
                    Title = "Treninzi od 21.12.2019 do 7.1.2020.",
                    Content =
                        "Treninzi u navedenom razdoblju održavat će se u dogovoru s trenerom ekipe, sve upite možete poslati na mail kluba. ",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.MaxValue,
                    Image = context.Image.Skip(2).First(),
                    PostType = "Obavijest"
                }
            };
            return posts;
        }

        private static IEnumerable<Player> GetPlayers(RudesDatabaseContext context)
        {
            string dateFormat = "yyyy-MM-dd";
            var players = new List<Player>
            {
                new Player
                {
                    Name = "Jurica",
                    LastName = "Nakić",
                    Position = PlayerPositionType.SG,
                    PlayerType = PlayerType.Seniors,
                    BirthDate = DateTime.ParseExact("1998-12-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Marko",
                    LastName = "Jelić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Seniors,
                    BirthDate = DateTime.ParseExact("1998-01-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Bruno",
                    LastName = "Grmača",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Seniors,
                    BirthDate = DateTime.ParseExact("1998-05-06", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Filip",
                    LastName = "Galić",
                    Position = PlayerPositionType.PF,
                    PlayerType = PlayerType.Seniors,
                    BirthDate = DateTime.ParseExact("1998-06-20", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Ivan",
                    LastName = "Ćelić",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.Seniors,
                    BirthDate = DateTime.ParseExact("1998-08-13", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Petar",
                    LastName = "Jeramaz",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.Seniors,
                    BirthDate = DateTime.ParseExact("1998-03-09", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Josip",
                    LastName = "Kapović",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Seniors,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Maksimilijan",
                    LastName = "Božić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Seniors,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Jurica",
                    LastName = "Nakić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Juniors,
                    BirthDate = DateTime.ParseExact("1998-12-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Marko",
                    LastName = "Jelić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Juniors,
                    BirthDate = DateTime.ParseExact("1998-01-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Bruno",
                    LastName = "Grmača",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Juniors,
                    BirthDate = DateTime.ParseExact("1998-05-06", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Filip",
                    LastName = "Galić",
                    Position = PlayerPositionType.PF,
                    PlayerType = PlayerType.Juniors,
                    BirthDate = DateTime.ParseExact("1998-06-20", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Ivan",
                    LastName = "Ćelić",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.Juniors,
                    BirthDate = DateTime.ParseExact("1998-08-13", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Petar",
                    LastName = "Jeramaz",
                    Position = PlayerPositionType.PG,
                    PlayerType = PlayerType.Juniors,
                    BirthDate = DateTime.ParseExact("1998-03-09", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Josip",
                    LastName = "Kapović",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Juniors,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Maksimilijan",
                    LastName = "Božić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Juniors,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Jurica",
                    LastName = "Nakić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Cadets,
                    BirthDate = DateTime.ParseExact("1998-12-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Marko",
                    LastName = "Jelić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Cadets,
                    BirthDate = DateTime.ParseExact("1998-01-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Bruno",
                    LastName = "Grmača",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Cadets,
                    BirthDate = DateTime.ParseExact("1998-05-06", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Filip",
                    LastName = "Galić",
                    Position = PlayerPositionType.PF,
                    PlayerType = PlayerType.Cadets,
                    BirthDate = DateTime.ParseExact("1998-06-20", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Ivan",
                    LastName = "Ćelić",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.Cadets,
                    BirthDate = DateTime.ParseExact("1998-08-13", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Petar",
                    LastName = "Jeramaz",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.Cadets,
                    BirthDate = DateTime.ParseExact("1998-03-09", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Josip",
                    LastName = "Kapović",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Cadets,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Maksimilijan",
                    LastName = "Božić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.Cadets,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Jurica",
                    LastName = "Nakić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.YoungCadets,
                    BirthDate = DateTime.ParseExact("1998-12-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Marko",
                    LastName = "Jelić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.YoungCadets,
                    BirthDate = DateTime.ParseExact("1998-01-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Bruno",
                    LastName = "Grmača",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.YoungCadets,
                    BirthDate = DateTime.ParseExact("1998-05-06", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Filip",
                    LastName = "Galić",
                    Position = PlayerPositionType.PF,
                    PlayerType = PlayerType.YoungCadets,
                    BirthDate = DateTime.ParseExact("1998-06-20", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Ivan",
                    LastName = "Ćelić",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.YoungCadets,
                    BirthDate = DateTime.ParseExact("1998-08-13", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Petar",
                    LastName = "Jeramaz",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.YoungCadets,
                    BirthDate = DateTime.ParseExact("1998-03-09", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Josip",
                    LastName = "Kapović",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.YoungCadets,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Maksimilijan",
                    LastName = "Božić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.YoungCadets,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Jurica",
                    LastName = "Nakić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.SportSchools,
                    BirthDate = DateTime.ParseExact("1998-12-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Marko",
                    LastName = "Jelić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.SportSchools,
                    BirthDate = DateTime.ParseExact("1998-01-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Bruno",
                    LastName = "Grmača",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.SportSchools,
                    BirthDate = DateTime.ParseExact("1998-05-06", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Filip",
                    LastName = "Galić",
                    Position = PlayerPositionType.PF,
                    PlayerType = PlayerType.SportSchools,
                    BirthDate = DateTime.ParseExact("1998-06-20", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Ivan",
                    LastName = "Ćelić",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.SportSchools,
                    BirthDate = DateTime.ParseExact("1998-08-13", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Petar",
                    LastName = "Jeramaz",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.SportSchools,
                    BirthDate = DateTime.ParseExact("1998-03-09", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Josip",
                    LastName = "Kapović",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.SportSchools,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Maksimilijan",
                    LastName = "Božić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.SportSchools,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Jurica",
                    LastName = "Nakić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.MiniBasketball,
                    BirthDate = DateTime.ParseExact("1998-12-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Marko",
                    LastName = "Jelić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.MiniBasketball,
                    BirthDate = DateTime.ParseExact("1998-01-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Bruno",
                    LastName = "Grmača",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.MiniBasketball,
                    BirthDate = DateTime.ParseExact("1998-05-06", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Filip",
                    LastName = "Galić",
                    Position = PlayerPositionType.PF,
                    PlayerType = PlayerType.MiniBasketball,
                    BirthDate = DateTime.ParseExact("1998-06-20", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Ivan",
                    LastName = "Ćelić",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.MiniBasketball,
                    BirthDate = DateTime.ParseExact("1998-08-13", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Petar",
                    LastName = "Jeramaz",
                    Position = PlayerPositionType.SF,
                    PlayerType = PlayerType.MiniBasketball,
                    BirthDate = DateTime.ParseExact("1998-03-09", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Josip",
                    LastName = "Kapović",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.MiniBasketball,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Maksimilijan",
                    LastName = "Božić",
                    Position = PlayerPositionType.C,
                    PlayerType = PlayerType.MiniBasketball,
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                }
            };

            return players;
        }

        private static IEnumerable<Match> GetMatches()
        {
            var matches = new List<Match>
            {
                new Match
                {
                    City = "Metković",
                    Country = "Hrvatska",
                    Time = DateTime.Now.AddDays(137),
                    HomeTeam = "KK Metković",
                    AwayTeam = "KK Rudeš",
                    SportsHall = "Sportska dvorana Metković"
                },
                new Match
                {
                    City = "Zagreb",
                    Country = "Hrvatska",
                    Time = DateTime.ParseExact("03.12.2019 20:15", "dd.MM.yyyy HH:mm",
                        CultureInfo.InvariantCulture),
                    HomeTeam = "KK Sesvete",
                    AwayTeam = "KK Rudeš",
                    SportsHall = "SŠ Jelkovac"
                }
            };
            return matches;
        }

        public static readonly string DummyPassword = "HiveMind.2019";

        public static readonly User DummyUserWithAllRoles = new User
        {
            UserName = "mail@hivemind.org",
            Email = "mail@hivemind.org",
            Name = "Hive",
            LastName = "Mind",
            EmailConfirmed = true
        };

        public static readonly User DummyUser = new User
        {
            UserName = "user@hivemind.org",
            Email = "user@hivemind.org",
            Name = "Hive",
            LastName = "Mind",
            EmailConfirmed = true
        };

        public static readonly User DummyCoachUser = new User
        {
            UserName = "coach@hivemind.org",
            Email = "coach@hivemind.org",
            Name = "Hive",
            LastName = "Mind",
            EmailConfirmed = true
        };

        public static readonly User DummyBoardUser = new User
        {
            UserName = "board@hivemind.org",
            Email = "board@hivemind.org",
            Name = "Hive",
            LastName = "Mind",
            EmailConfirmed = true
        };

        public static readonly User DummyAdminUser = new User
        {
            UserName = "admin@hivemind.org",
            Email = "admin@hivemind.org",
            Name = "Hive",
            LastName = "Mind",
            EmailConfirmed = true
        };


        private static List<User> GetUsers()
        {
            return new List<User>
            {
                DummyUser, DummyCoachUser, DummyBoardUser, DummyAdminUser, DummyUserWithAllRoles
            };
        }

        private static void CreateUsers(IEnumerable<User> users, UserManager<User> userManager)
        {
            if (users.Select(user => userManager.CreateAsync(user, DummyPassword).Result)
                .Any(result => !result.Succeeded))
            {
                throw (new Exception("Could not create dummy user..."));
            }
        }

        private static IEnumerable<Review> GetReviews(RudesDatabaseContext context)
        {
            var reviews = new List<Review>
            {
                new Review
                {
                    Article = context.Article.First(),
                    Comment = "Vrlo odlično spektakularno!! <3",
                    Rating = 5,
                    User = context.User.First()
                },
                new Review
                {
                    Article = context.Article.First(),
                    Comment = "Spektakularno!! Kupija san dvaput",
                    Rating = 5,
                    User = context.User.First()
                },
                new Review
                {
                    Article = context.Article.Skip(1).First(),
                    Comment = "A nije nešto :/",
                    Rating = 3,
                    User = context.User.First()
                }
            };

            return reviews;
        }

        private static IEnumerable<ShoppingCartArticle> GetShoppingCartArticles(RudesDatabaseContext context)
        {
            var shoppingCartArticles = new List<ShoppingCartArticle>
            {
                new ShoppingCartArticle
                {
                    ShoppingCart = context.ShoppingCart.First(),
                    Article = context.Article.First(),
                    Size = "L",
                    Quantity = 3
                },
                new ShoppingCartArticle
                {
                    ShoppingCart = context.ShoppingCart.First(),
                    Article = context.Article.Skip(1).First(),
                    Size = "M",
                    Quantity = 3
                },
                new ShoppingCartArticle
                {
                    ShoppingCart = context.ShoppingCart.First(),
                    Article = context.Article.Skip(2).First(),
                    Size = "L",
                    Quantity = 1
                }
            };

            return shoppingCartArticles;
        }


        private static IEnumerable<Order> GetOrders(RudesDatabaseContext context)
        {
            var orders = new List<Order>
            {
                new Order
                {
                    User = context.User.Skip(1).First(),
                    Address = "Kralja Zvonimira 32",
                    PostalCode = 20350
                },
                new Order
                {
                    User = context.User.Skip(1).First(),
                    Address = "Kralja Zvonimira 32",
                    PostalCode = 20350
                }
            };

            return orders;
        }

        private static IEnumerable<OrderArticle> GetOrderArticles(RudesDatabaseContext context)
        {
            var orderArticles = new List<OrderArticle>
            {
                new OrderArticle
                {
                    Order = context.Order.First(),
                    Article = context.Article.First(),
                    Size = "L",
                    Quantity = 1,
                    PurchaseDiscount = 0,
                    PurchasePrice = context.Article.First().Price
                },
                new OrderArticle
                {
                    Order = context.Order.First(),
                    Article = context.Article.Skip(1).First(),
                    Size = "M",
                    Quantity = 1,
                    PurchaseDiscount = 0,
                    PurchasePrice = context.Article.Skip(1).First().Price
                },
                new OrderArticle
                {
                    Order = context.Order.First(),
                    Article = context.Article.Skip(1).First(),
                    Size = "S",
                    Quantity = 3,
                    PurchaseDiscount = 0,
                    PurchasePrice = context.Article.Skip(1).First().Price
                },
                new OrderArticle
                {
                    Order = context.Order.Skip(1).First(),
                    Article = context.Article.First(),
                    Size = "L",
                    Quantity = 1,
                    PurchaseDiscount = 0,
                    PurchasePrice = context.Article.First().Price
                },
                new OrderArticle
                {
                    Order = context.Order.Skip(1).First(),
                    Article = context.Article.Skip(1).First(),
                    Size = "M",
                    Quantity = 1,
                    PurchaseDiscount = 0,
                    PurchasePrice = context.Article.Skip(1).First().Price
                },
                new OrderArticle
                {
                    Order = context.Order.Skip(1).First(),
                    Article = context.Article.Skip(2).First(),
                    Size = "S",
                    Quantity = 3,
                    PurchaseDiscount = 0,
                    PurchasePrice = context.Article.Skip(2).First().Price
                }
            };

            return orderArticles;
        }

        private static async Task SetupUserRoles(IServiceProvider serviceProvider)
        {
            var userRoles = new Dictionary<string, string[]>()
            {
                {Roles.Admin, new[] {DummyAdminUser.Email, DummyUserWithAllRoles.Email}},
                {Roles.Board, new[] {DummyBoardUser.Email, DummyUserWithAllRoles.Email}},
                {Roles.Coach, new[] {DummyCoachUser.Email, DummyUserWithAllRoles.Email}},
                {Roles.User, new[] {DummyUser.Email, DummyUserWithAllRoles.Email}}
            };

            foreach (var (roleName, usersInRole) in userRoles)
            {
                await SetupRole(roleName, usersInRole, serviceProvider);
            }
        }

        private static async Task SetupRole(string roleName, string[] userIDs, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var roleCheck = await roleManager.RoleExistsAsync(roleName);
            if (!roleCheck)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            foreach (string userId in userIDs)
            {
                User user = await userManager.FindByEmailAsync(userId);
                var roles = await userManager.GetRolesAsync(user);
                foreach(var role in roles)
                {
                    await userManager.RemoveFromRoleAsync(user, role);
                }
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}