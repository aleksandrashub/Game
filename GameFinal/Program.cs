



using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Game;
namespace Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<GameCharacter> characters = new List<GameCharacter>();

            List<GameCharacter> dead = new List<GameCharacter>();
            int nomerMenu;
            int pobed;
            int indexChar = 1;
            int createOrChoose = 1;
            int otvetMenu = 1;
            int otvetMenuGame = 1;




            GameCharacter character1 = new GameCharacter();
            characters.Add(character1);
            character1.menu(1, characters, dead);
            indexChar = characters.IndexOf(character1);
            nomerMenu = character1.nomer;

            while (otvetMenu >= 1 && otvetMenu <= 6)
            {
                Console.WriteLine("Меню персонажа номер " + nomerMenu);
                Console.WriteLine(" 1 - показать информацию о себе \n 2 - посмотреть список друзей \n 3 - начать игру  \n 4 - переключиться на другого персонажа или создать нового");
                otvetMenu = Convert.ToInt32(Console.ReadLine());
                switch (otvetMenu)
                {
                    case 1:
                        characters[indexChar].menu(2, characters, dead);
                        break;

                    case 2:
                        characters[indexChar].menu(6, characters, dead);
                        break;

                    case 4:
                        Console.WriteLine("Введите 0, если хотите выбрать существующего персонажа или введите 1, чтобы создать нового");
                        createOrChoose = Convert.ToInt32(Console.ReadLine());

                        if (createOrChoose == 1)
                        {
                            GameCharacter character = new GameCharacter();
                            characters.Add(character);
                            character.menu(1, characters, dead);
                            indexChar = characters.IndexOf(character);
                            nomerMenu = character.nomer;

                        }
                        else if (createOrChoose == 0)
                        {
                            Console.WriteLine("У вас " + characters.Count + " персонажей. Введите номер персонажа (1, 2...), чтобы перейти в его меню");
                            nomerMenu = Convert.ToInt32(Console.ReadLine());
                            bool check = false;

                            foreach (GameCharacter character in characters)
                            {
                                if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                {
                                    indexChar = characters.IndexOf(character);
                                    nomerMenu = character.nomer;
                                    check = true;
                                }
                            }
                            while (check == false)
                            {
                                Console.WriteLine("Персонажа под этим номером нет. Введите заново");
                                nomerMenu = Convert.ToInt32(Console.ReadLine());
                                foreach (GameCharacter character in characters)
                                {
                                    if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                    {
                                        indexChar = characters.IndexOf(character);
                                        nomerMenu = character.nomer;
                                        check = true;
                                    }
                                }

                            }

                        }

                        break;


                    case 3:
                        characters[indexChar].menu(5, characters, dead);
                        otvetMenuGame = 1;

                        pobed = characters[indexChar].winProv(characters, dead);

                        if (pobed != 2 && pobed != 1 && pobed != 3 && dead.Contains(characters[indexChar]) == true)
                        {
                          //  characters.Remove(characters[indexChar]);

                            Console.WriteLine("У вас " + (characters.Count-dead.Count) + " персонажей. Введите номер персонажа, чтобы перейти в его меню");
                            nomerMenu = Convert.ToInt32(Console.ReadLine());
                            bool checkNom1 = false;

                            foreach (GameCharacter character in characters)
                            {
                                if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                {
                                    indexChar = characters.IndexOf(character);
                                    checkNom1 = true;
                                }
                            }
                            while (checkNom1 == false)
                            {
                                Console.WriteLine("Персонажа под этим номером нет. Введите заново");
                                nomerMenu = Convert.ToInt32(Console.ReadLine());
                                foreach (GameCharacter character in characters)
                                {
                                    if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                    {
                                        indexChar = characters.IndexOf(character);
                                        checkNom1 = true;
                                    }
                                }

                            }

                            otvetMenuGame = 1;
                        }
                        else if (pobed == 1)
                        {
                            Console.WriteLine("Ваша команда победила! Игра завершена");
                            otvetMenu = 0;
                            createOrChoose = 2;
                            otvetMenuGame = 0;
                        }
                        else if (pobed == 2)
                        {
                            Console.WriteLine("Ваша команда мертва! Игра завершена");
                            otvetMenu = 0;
                            createOrChoose = 2;
                            otvetMenuGame = 0;

                        }
                        else if (pobed == 3)
                        {
                            Console.WriteLine("Обе команды мертвы!");
                            otvetMenu = 0;
                            createOrChoose = 2;
                            otvetMenuGame = 0;

                        }

                        else
                        {
                            while (otvetMenuGame >= 1 && otvetMenuGame <= 7)
                            {
                                Console.WriteLine("Меню персонажа номер " + nomerMenu);
                                Console.WriteLine(" 1 - показать информацию о себе \n 2 - переместить по горизонтали \n 3 - перемеситить по вертикали \n 4 - посмотреть список друзей \n 5 -  лечение друга \n 6 - поробовать себя вылечить \n 7 -переключиться на другого персонажа ");
                                otvetMenuGame = Convert.ToInt32(Console.ReadLine());
                                switch (otvetMenuGame)
                                {
                                    case 1:
                                        characters[indexChar].menu(2, characters, dead); // вывод информации
                                        break;

                                    case 2:
                                        characters[indexChar].menu(2);
                                        characters[indexChar].menu(5, characters, dead);//проверка на столкновение
                                        pobed = characters[indexChar].winProv(characters, dead);
                                        if (dead.Contains(characters[indexChar]) && pobed != 1 && pobed != 2 && pobed != 3)
                                        {
                                            Console.WriteLine("У вас " + (characters.Count - dead.Count) + " персонажей. Введите номер персонажа, чтобы перейти в его меню");
                                            nomerMenu = Convert.ToInt32(Console.ReadLine());
                                            bool checkNom1 = false;

                                            foreach (GameCharacter character in characters)
                                            {
                                                if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                                {
                                                    indexChar = characters.IndexOf(character);
                                                    checkNom1 = true;
                                                }
                                            }
                                            while (checkNom1 == false)
                                            {
                                                Console.WriteLine("Персонажа под этим номером нет. Введите заново");
                                                nomerMenu = Convert.ToInt32(Console.ReadLine());
                                                foreach (GameCharacter character in characters)
                                                {
                                                    if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                                    {
                                                        indexChar = characters.IndexOf(character);
                                                        checkNom1 = true;
                                                    }
                                                }

                                            }
                                        }



                                        else if (pobed == 1)
                                        {
                                            Console.WriteLine("Ваша команда победила! Игра завершена");
                                            otvetMenu = 0;
                                            createOrChoose = 2;
                                            otvetMenuGame = 0;
                                        }
                                        else if (pobed == 2)
                                        {
                                            Console.WriteLine("Ваша команда мертва! Игра завершена");
                                            otvetMenu = 0;
                                            createOrChoose = 2;
                                            otvetMenuGame = 0;

                                        }
                                        else if (pobed == 3)
                                        {
                                            Console.WriteLine("Обе команды мертвы!");
                                            otvetMenu = 0;
                                            createOrChoose = 2;
                                            otvetMenuGame = 0;

                                        }

                                        break;

                                    case 3:
                                        characters[indexChar].menu(3);
                                        characters[indexChar].menu(5, characters, dead);//проверка на столкновение

                                        pobed = characters[indexChar].winProv(characters, dead);
                                        if (dead.Contains(characters[indexChar]) && pobed != 1 && pobed != 2 && pobed != 3)
                                        {
                                            Console.WriteLine("У вас " + (characters.Count - dead.Count) + " персонажей. Введите номер персонажа, чтобы перейти в его меню");
                                            nomerMenu = Convert.ToInt32(Console.ReadLine());
                                            bool checkNom1 = false;

                                            foreach (GameCharacter character in characters)
                                            {
                                                if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                                {
                                                    indexChar = characters.IndexOf(character);
                                                    checkNom1 = true;
                                                }
                                            }
                                            while (checkNom1 == false)
                                            {
                                                Console.WriteLine("Персонажа под этим номером нет. Введите заново");
                                                nomerMenu = Convert.ToInt32(Console.ReadLine());
                                                foreach (GameCharacter character in characters)
                                                {
                                                    if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                                    {
                                                        indexChar = characters.IndexOf(character);
                                                        checkNom1 = true;
                                                    }
                                                }

                                            }
                                        }

                                        if (pobed == 1)
                                        {
                                            Console.WriteLine("Ваша команда победила! Игра завершена");
                                            otvetMenu = 0;
                                            createOrChoose = 2;
                                            otvetMenuGame = 0;
                                            break;
                                        }
                                        else if (pobed == 2)
                                        {
                                            Console.WriteLine("Ваша команда мертва! Игра завершена");
                                            otvetMenu = 0;
                                            createOrChoose = 2;
                                            otvetMenuGame = 0;
                                            break;
                                        }
                                        else if (pobed == 3)
                                        {
                                            Console.WriteLine("Обе команды мертвы!");
                                            otvetMenu = 0;
                                            createOrChoose = 2;
                                            otvetMenuGame = 0;
                                            break;
                                        }


                                        break;

                                    case 4:
                                        characters[indexChar].menu(6, characters, dead);// просмотр номеров друзей
                                        break;

                                    case 5:
                                        characters[indexChar].menu(4, characters, dead); // лечение друзей 
                                        break;
                                    case 6:
                                        characters[indexChar].menu(7, characters, dead); //восстановить себя
                                        break;

                                    case 7:
                                        Console.WriteLine("У вас " + (characters.Count - dead.Count) + " персонажей. Введите номер персонажа (1, 2...), чтобы перейти в его меню");
                                        nomerMenu = Convert.ToInt32(Console.ReadLine());
                                        bool checkNom = false;

                                        foreach (GameCharacter character in characters)
                                        {
                                            if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                            {
                                                indexChar = characters.IndexOf(character);
                                                nomerMenu = character.nomer;
                                                checkNom = true;
                                            }
                                        }
                                        while (checkNom == false)
                                        {
                                            Console.WriteLine("Персонажа под этим номером нет. Введите заново");
                                            nomerMenu = Convert.ToInt32(Console.ReadLine());
                                            foreach (GameCharacter character in characters)
                                            {
                                                if (nomerMenu == character.nomer && dead.Contains(character) == false)
                                                {
                                                    indexChar = characters.IndexOf(character);
                                                    nomerMenu = character.nomer;
                                                    checkNom = true;
                                                }


                                            }


                                        }
                                        characters[indexChar].menu(5, characters, dead);
                                        otvetMenuGame = 1;
                                        break;

                                }

                            }





                        }
                        break;

                }
            }

            Console.ReadLine();


        }


    }

}



