using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab10
{
    public class Question
    {
        public List<QuestionModel> data = new List<QuestionModel>();
        public QuestionModel quest = new QuestionModel();
        
        public void FillAll()
        {
            data.Add(new QuestionModel()
            {
                CategoryId = 1,
                CorrectAnswer = "apple",
                ImageUrl = "https://t4.ftcdn.net/jpg/02/52/93/81/360_F_252938192_JQQL8VoqyQVwVB98oRnZl83epseTVaHe.jpg"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 1,
                CorrectAnswer = "carrot",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT1Skns2cZO0nZI7IEgJJPL9b2UH_xxk6ml5k-mSxLJyg&s"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 1,
                CorrectAnswer = "lime",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTG_aRhS4wWTjy7dMl1IXUvsA7nOAl3Xxb4LYOvkLwf0g&s"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 1,
                CorrectAnswer = "potato",
                ImageUrl = "https://media.istockphoto.com/id/1189117812/vector/vector-illustration-of-a-funny-potato-in-cartoon-style.jpg?s=612x612&w=0&k=20&c=rubtZdWlljpwMKKcpzM7uylkd42B9sVPiDk40onTq3Y="
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 1,
                CorrectAnswer = "orange",
                ImageUrl = "https://img.freepik.com/free-vector/hand-drawn-colorful-orange-illustration_53876-2977.jpg"
            });


            //plants
            data.Add(new QuestionModel()
            {
                CategoryId = 2,
                CorrectAnswer = "rose",
                ImageUrl = "https://www.shutterstock.com/image-photo/red-rose-isolated-on-white-260nw-232367245.jpg"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 2,
                CorrectAnswer = "iris",
                ImageUrl = "https://www.trianglenursery.co.uk/pictures/flower-guide/Iris.jpg"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 2,
                CorrectAnswer = "daisy",
                ImageUrl = "https://w7.pngwing.com/pngs/771/101/png-transparent-common-daisy-flower-daisy-family-transvaal-daisy-small-daisy-plant-stem-chamomile-tulip-thumbnail.png"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 2,
                CorrectAnswer = "fir",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR_hxUfRBhmj-mIHUPKYPZp33AT8BcIWo6QuSB5lC7MfA&s"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 2,
                CorrectAnswer = "cactus",
                ImageUrl = "https://images.unsplash.com/photo-1554631221-f9603e6808be?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y2FjdHVzJTIwcGxhbnR8ZW58MHx8MHx8fDA%3D"
            });

            //animals

            data.Add(new QuestionModel()
            {
                CategoryId = 3,
                CorrectAnswer = "cat",
                ImageUrl = "https://media.istockphoto.com/id/119574557/photo/ginger-cat-sitting-in-front-of-white-backdrop.jpg?s=612x612&w=0&k=20&c=drO4kqqFUQZ_n3p55LuU3FSFZCKqqve0H3h1G8DU11c="
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 3,
                CorrectAnswer = "rat",
                ImageUrl = "https://www.flapest.com/wp-content/uploads/2021/02/norway-rat.jpg"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 3,
                CorrectAnswer = "goat",
                ImageUrl = "https://i.pinimg.com/736x/44/dc/99/44dc99df7772f9badd3dc3f97e742bad.jpg"
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 3,
                CorrectAnswer = "rabbit",
                ImageUrl = "https://media.istockphoto.com/id/518322790/photo/baby-of-orange-rabbit.webp?b=1&s=612x612&w=0&k=20&c=eqTQjf8q57zQ4Gi9DDyO7XkQZKe3RKVONWvEWwOYEGQ="
            });

            data.Add(new QuestionModel()
            {
                CategoryId = 3,
                CorrectAnswer = "frog",
                ImageUrl = "https://media.istockphoto.com/id/175397603/photo/frog.jpg?s=612x612&w=0&k=20&c=EMXlwg5SicJllr7gnSFUUjzwCGa1ciLjYD1bk8NvO2E="
            });
        }


        public void FilterByCategory(string CategoryName)
        {
            if (CategoryName == "Еда")
            {
                data = data.Where(p => p.CategoryId == 1).ToList();
            }
            else if (CategoryName == "Растения")
            {
                data = data.Where(p => p.CategoryId == 2).ToList();
            }
            else if (CategoryName == "Животные")
            {
                data = data.Where(p => p.CategoryId == 3).ToList();
            }
        }

        public void GenerateQuestion()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, data.Count);
            quest = data[index];
            data.Remove(quest);
        }

        public string GetQuestionImage()
        {
            return quest.ImageUrl;
        }

        public bool CheckAnswer(string answ)
        {
            return answ.ToLower() == quest.CorrectAnswer;
        }
    }
}
