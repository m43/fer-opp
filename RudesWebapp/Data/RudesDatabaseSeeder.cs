using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RudesWebapp.Models;

namespace RudesWebapp.Data
{
    public class RudesDatabaseSeeder
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<RudesDatabaseContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate(); // TODO not sure if needed

                var signInManager = serviceScope.ServiceProvider.GetService<SignInManager<User>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();


                if (context.Article != null && context.Article.Any())
                    return; // database already seeded

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
                    context.SaveChanges();
                }

                var posts = GetPosts(context);
                context.Post.AddRange(posts);
                context.SaveChanges();

                var players = GetPlayers(context);
                context.Player.AddRange(players);
                context.SaveChanges();

                var matches = GetMatches();
                context.Match.AddRange(matches);
                context.SaveChanges();

                CreateUser(context, userManager, signInManager);
                context.SaveChanges();

                var shopingCarts = GetShoppingCarts(context);
                context.ShoppingCart.AddRange(shopingCarts);
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

                // TODO fix transactions..
                // var transactions = GetTransactions(context);
                // context.Transaction.AddRange(transactions);
                // // context.SaveChanges();

                context.SaveChanges();
            }
        }

        public static List<Image> GetImages()
        {
            var images = new List<Image>()
            {
                new Image
                {
                    Path =
                        "https://www.maxim.com/.image/c_fit%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_620/MTM1MTQzNDQ0MDQ1NzAzODEx/placeholder-title.jpg"
                },
                new Image
                {
                    Path =
                        "https://www.maxim.com/.image/c_fit%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_620/MTM1MTQzNDQ0MDQ1NzAzODEx/placeholder-title.jpg"
                },
                new Image
                {
                    Path =
                        "https://www.maxim.com/.image/c_fit%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_620/MTM1MTQzNDQ0MDQ1NzAzODEx/placeholder-title.jpg"
                },
                new Image
                {
                    Path =
                        "https://www.maxim.com/.image/c_fit%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_620/MTM1MTQzNDQ0MDQ1NzAzODEx/placeholder-title.jpg"
                }
            };
            return images;
        }

        public static List<Article> GetArticles(RudesDatabaseContext context)
        {
            List<Article> articles = new List<Article>()
            {
                new Article
                {
                    Name = "Special Christmas Hoodie",
                    Description =
                        "Super special red hoodie with Rudes logo. Rudes Hoodie izrađena je od kvalitetne 320 gr felpe. Topla i ugodna, idealan je izbor za sve sportaše kao i za navijače. Veliki prednji džep te vezice na ovratniku. ",
                    Image = context.Image.First(),
                    Price = 250,
                    Type = "hoodie",
                },
                new Article
                {
                    Name = "Anniversary T-shirt",
                    Description =
                        "Rare anniversary T-shirt. Machine wash cold with like colors, dry low heat. Solid colors: 100% Cotton; Heather Grey: 90% Cotton, 10% Polyester; All Other Heathers: 50% Cotton, 50% Polyester.",
                    Image = context.Image.Skip(1).First(),
                    Price = 200,
                    Type = "t-shirt",
                },
                new Article
                {
                    Name = "T-shirt",
                    Description =
                        "Rudes majica izrađena je od 100% pamuka s tiskanim motivom na prednjoj strani. Pravi izbor za sve navijače i simpatizere maksimirskog kluba.",
                    Price = 120,
                    Type = "t-shirt"
                },
                new Article
                {
                    Name = "Stone long sleeve muscle fit ribbed T-shirt",
                    Image = context.Image.Skip(2).First(),
                    Price = 120,
                    Type = "t-shirt"
                }
            };
            return articles;
        }

        public static List<Discount> GetDiscounts(RudesDatabaseContext context)
        {
            List<Discount> discounts = new List<Discount>()
            {
                new Discount
                {
                    Article = context.Article.First(),
                    Percentage = 30
                },
                new Discount
                {
                    Article = context.Article.Skip(1).First(),
                    Percentage = 15
                }
            };

            return discounts;
        }

        public static List<ArticleAvailability> GetArticleAvailabilities(RudesDatabaseContext context)
        {
            List<ArticleAvailability> articleAvailabilities = new List<ArticleAvailability>
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

        public static List<Post> GetPosts(RudesDatabaseContext context)
        {
            List<Post> posts = new List<Post>
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
                }
            };
            return posts;
        }

        public static List<Player> GetPlayers(RudesDatabaseContext context)
        {
            String dateFormat = "yyyy-MM-dd";
            List<Player> players = new List<Player>
            {
                new Player
                {
                    Name = "Jurica",
                    LastName = "Nakić",
                    Position = "Centar",
                    BirthDate = DateTime.ParseExact("1998-12-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },


                new Player
                {
                    Name = "Marko",
                    LastName = "Jelić",
                    Position = "Centar",
                    BirthDate = DateTime.ParseExact("1998-01-21", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },

                new Player
                {
                    Name = "Bruno",
                    LastName = "Grmača",
                    Position = "Nisko krilo",
                    BirthDate = DateTime.ParseExact("1998-05-06", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "FIlip",
                    LastName = "Galić",
                    Position = "Krilni centar",
                    BirthDate = DateTime.ParseExact("1998-06-20", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Ivan",
                    LastName = "Ćelić",
                    Position = "Bek šuter",
                    BirthDate = DateTime.ParseExact("1998-08-13", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Petar",
                    LastName = "Jeramaz",
                    Position = "Bek šuter",
                    BirthDate = DateTime.ParseExact("1998-03-09", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                },
                new Player
                {
                    Name = "Josip",
                    LastName = "Kapović",
                    Position = "Nisko krilo",
                    BirthDate = DateTime.ParseExact("1998-11-22", dateFormat, CultureInfo.InvariantCulture),
                    Image = context.Image.First()
                }
            };

            return players;
        }

        public static List<Match> GetMatches()
        {
            List<Match> matches = new List<Match>
            {
                new Match
                {
                    City = "Metković",
                    Country = "Hrvatska",
                    Date = DateTime.Now.AddDays(137),
                    HomeTeam = "KK Metković",
                    AwayTeam = "KK Rudeš",
                    SportsHall = "Sportska dvorana Metković"
                },
                new Match
                {
                    City = "Zagreb",
                    Country = "Hrvatska",
                    Date = DateTime.ParseExact("03.12.2019 20:15", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    HomeTeam = "KK Sesvete",
                    AwayTeam = "KK Rudeš",
                    SportsHall = "SŠ Jelkovac"
                }
            };
            return matches;
        }

        public static void CreateUser(RudesDatabaseContext context, UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            var user = new User
            {
                UserName = "mail@hivemind.org",
                Email = "mail@hivemind.org",
                Name = "Hive",
                LastName = "Mind",
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, "HiveMind.2019").Result;
            if (!result.Succeeded)
            {
                throw (new Exception("Could not create dummy user..."));
            }
        }

        public static List<Review> GetReviews(RudesDatabaseContext context)
        {
            List<Review> reviews = new List<Review>
            {
                new Review
                {
                    Article = context.Article.First(),
                    Comment = "Vrlo odlično spektakularno!! <3",
                    Rating = 5,
                    UsernameNavigation = context.User.First()
                },
                new Review
                {
                    Article = context.Article.First(),
                    Comment = "Spektakularno!! Kupija san dvaput",
                    Rating = 5,
                    UsernameNavigation = context.User.First()
                },
                new Review
                {
                    Article = context.Article.Skip(1).First(),
                    Comment = "A nije nešto :/",
                    Rating = 3,
                    UsernameNavigation = context.User.First()
                }
            };

            return reviews;
        }

        public static List<ShoppingCart> GetShoppingCarts(RudesDatabaseContext context)
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>
            {
                new ShoppingCart
                {
                    UsernameNavigation = context.User.First()
                }
            };

            return shoppingCarts;
        }

        public static List<ShoppingCartArticle> GetShoppingCartArticles(RudesDatabaseContext context)
        {
            List<ShoppingCartArticle> shoppingCartArticles = new List<ShoppingCartArticle>
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


        public static List<Transaction> GetTransactions(RudesDatabaseContext context)
        {
            // TODO fix transactions..
            List<Transaction> transactions = new List<Transaction>
            {
//                new Transaction
//                {
//                    Amount = 1234,
//                    Card = "2030302012344321",
//                    Order = ? // TODO need to make Transaction <--> Order to be a one to one ralationship..
//                }
            };

            return transactions;
        }

        public static List<Order> GetOrders(RudesDatabaseContext context)
        {
            List<Order> orders = new List<Order>
            {
                new Order
                {
                    UsernameNavigation = context.User.First(),
                    Address = "Kralja Zvonimira 32",
                    PostalCode = 20350
                    // IdTransactionNavigation = context.Transaction // TODO ...
                }
            };

            return orders;
        }

        public static List<OrderArticle> GetOrderArticles(RudesDatabaseContext context)
        {
            var orderArticles = new List<OrderArticle>
            {
                new OrderArticle
                {
                    Order = context.Order.First(),
                    Article = context.Article.First(),
                    Size = "L",
                    Quantity = 1,
                    PurchaseDiscount = 30,
                    PurchasePrice = 120
                },
                new OrderArticle
                {
                    Order = context.Order.First(),
                    Article = context.Article.Skip(1).First(),
                    Size = "M",
                    Quantity = 1,
                    PurchaseDiscount = 15,
                    PurchasePrice = 150
                }
            };

            return orderArticles;
        }
    }
}