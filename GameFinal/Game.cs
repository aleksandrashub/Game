using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    public class GameCharacter
    {
        public int nomer;
        private string name;
        private int coordX;
        private int coordY;
        private bool lager;
        private int lifes;
        private int power;
        private int pobeda;

        public void menu(int otvet, List<GameCharacter> characters, List<GameCharacter> dead)
        {
            switch (otvet)
            {
                case 1:
                    this.info(this.name, this.coordX, this.coordY, this.lager, this.lifes, this.power, characters);
                    break;
                case 2:
                    this.print(dead);
                    break;
                case 6:
                    this.yourteam(characters, dead);
                    break;

                case 5:

                    this.gameStart(characters, dead);

                    break;
                case 4:

                    this.heal(characters, dead);

                    break;
                case 7:

                    if (this.pobeda / 5 >= 1)
                    {
                        Console.WriteLine("У вас достаточно выигрышей, чтобы полностью безвозмездно восстановить себя");

                        this.vosst(dead);

                    }
                    else
                    {
                        Console.WriteLine("У вас пока недостаточно побед");
                    }
                    break;


            }

        }
        public void menu(int otvet)
        {
            switch (otvet)
            {
                case 2:

                    this.moveX(this.coordX);
                    break;
                case 3:
                    this.moveY(this.coordY);
                    break;

            }

        }


        private void info(string name, int coordX, int coordY, bool lager, int lifes, int power, List<GameCharacter> characters)
        {
            Console.WriteLine("Введите имя персонажа:");
            this.name = Console.ReadLine();
            this.nomer = characters.IndexOf(this) + 1;
            Console.WriteLine("Введите местоположение Х:");
            this.coordX = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите местоположение У:");
            this.coordY = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите лагерь (добрый/злой):");
            string otvet = Console.ReadLine();
            switch (otvet)
            {
                case "добрый":
                    this.lager = true;


                    break;
                case "злой":
                    this.lager = false;

                    break;
            }


            this.lifes = 100;
            Console.WriteLine("Количество жизней 100");


            Random rnd = new Random();
            this.power = rnd.Next(30, 50);
            Console.WriteLine("Сила персонажа:" + this.power);
            Console.WriteLine("Количество побед: " + this.pobeda);


        }


        private void yourteam(List<GameCharacter> characters, List<GameCharacter> dead)
        {
            if (this.lager == true)
            {

                Console.WriteLine("Ваша команда Добрые. Порядковые номера живых участников:");
                foreach (GameCharacter character in characters)
                {
                    if (dead.Contains(character) == false && character.lager == true)
                    {
                        Console.WriteLine(character.nomer);
                    }

                }
            }
            else
            {

                Console.WriteLine("Ваша команда Злые. Порядковые номера живых участников:");
                foreach (GameCharacter character in characters)
                {
                    if (dead.Contains(character) == false && character.lager == false)
                    {
                        Console.WriteLine(character.nomer);
                    }

                }



            }

        }
        private void print(List<GameCharacter> dead)
        {

            Console.WriteLine(" Имя персонажа: " + this.name + "\n Местоположение х: " + this.coordX + " y: " + this.coordY);
            Console.WriteLine("Номер: " + this.nomer);
            if (this.lager == true)
            {
                Console.WriteLine(" Лагерь: добрые");
            }
            else
            {
                Console.WriteLine(" Лагерь: злые");

            }
            Console.WriteLine(" Количество жизней: " + this.lifes);
            Console.WriteLine("Сила персонажа: " + this.power);
            Console.WriteLine("Количество побед: " + this.pobeda);
            if (dead.Contains(this) == true)
            {
                Console.WriteLine("Статус: мертв");
            }
            else
            {
                Console.WriteLine("Статус: жив");
            }

        }


        private void gameStart(List<GameCharacter> characters, List<GameCharacter> dead)
        {
            List<int> enIndex = new List<int>();
            List<int> frIndex = new List<int>();
            for (int i = 0; i < characters.Count; i++)
            {

                if (this != characters[i] && lagerProv(characters[i], this) == false && this.coordX == characters[i].coordX && this.coordY == characters[i].coordY && dead.Contains(characters[i]) == false && dead.Contains(this) == false)

                {
                    enIndex.Add(i);
                }
                else if (this != characters[i] && lagerProv(characters[i], this) == true && this.coordX == characters[i].coordX && this.coordY == characters[i].coordY && dead.Contains(characters[i]) == false && dead.Contains(this) == false)
                {
                    frIndex.Add(i);
                }
            }
            int obshayaSilaEn = 0;
            int obshayaSilaFr = 0;
            if (enIndex.Count > 1)
            {
                Console.WriteLine("Вы встретились с врагами под номерами ");
                foreach (int i in enIndex)
                {
                    Console.WriteLine(characters[i].nomer);
                    obshayaSilaEn += characters[i].power;
                }
                Console.WriteLine("Их общая сила равна " + obshayaSilaEn);
            }
            else if (enIndex.Count == 1)
            {
                Console.WriteLine("Вы встретили врага под номером " + (enIndex[0] + 1));
                obshayaSilaEn = characters[enIndex[0]].power;
                Console.WriteLine("Его сила равна " + obshayaSilaEn);
            }
            if (frIndex.Count > 1 && enIndex.Count > 0)
            {

                Console.WriteLine("Вы встретились с друзьями под номерами ");
                foreach (int i in frIndex)
                {
                    Console.WriteLine(characters[i].nomer);
                    obshayaSilaFr += characters[i].power;
                }
                obshayaSilaFr += this.power;
                Console.WriteLine("Ваша общая сила равна " + obshayaSilaFr);
            }
            else if (frIndex.Count == 1 && enIndex.Count > 0)
            {
                Console.WriteLine("Вы встретили друга под номером " + (characters[frIndex[0]].nomer));
                obshayaSilaFr = characters[frIndex[0]].power + this.power;
                Console.WriteLine("Ваша общая сила равна " + obshayaSilaFr);
            }
            else if (frIndex.Count == 0 && enIndex.Count > 0)
            {
                obshayaSilaFr = this.power;
                Console.WriteLine("Ваша сила :" + obshayaSilaFr);
            }


            if (enIndex.Count > 0)
            {
                Console.WriteLine("1 - нанести урон \n2 - полностью уничтожить врага ");
                int bitva = Convert.ToInt32(Console.ReadLine());
                switch (bitva)
                {

                    case 1:
                        int counten = 0;
                        int countfr = 0;
                        foreach (int i in enIndex)
                        {
                            uron(obshayaSilaFr / enIndex.Count, characters[i]);
                            if (characters[i].lifes <= 0)
                            {
                                counten++;

                            }
                        }

                        Console.WriteLine("Вы нанесли врагу(врагам) урон " + (obshayaSilaFr / enIndex.Count));
                        if (frIndex.Count > 0)
                        {
                            uron(obshayaSilaEn / (frIndex.Count + 1), this);
                            foreach (int i in frIndex)
                            {
                                uron(obshayaSilaEn / (frIndex.Count + 1), characters[i]);
                                if (characters[i].lifes <= 0)
                                {
                                    countfr++;
                                }
                            }
                            if (dead.Contains(this) == true)
                            {
                                countfr += 1;
                            
                            }
                            Console.WriteLine("Враг нанес каждому урон " + (obshayaSilaEn / (frIndex.Count + 1)));
                        }
                        else
                        {
                            uron(obshayaSilaEn, this);
                            Console.WriteLine("Враг нанес вам урон " + obshayaSilaEn);
                        }




                        if (obshayaSilaFr / enIndex.Count > obshayaSilaEn / (frIndex.Count + 1) || counten > countfr )
                        {
                            foreach (int i in enIndex)
                            {
                                if (characters[i].lifes <= 0)
                                {
                                    characters[i].lifes = 0;

                                    dead.Add(characters[i]);
                                 

                                    Console.WriteLine("Враг под номером " + (i + 1) + " мертв.");
                                }

                            }
                            if (this.lifes <= 0)
                            {
                                this.lifes = 0;
                                dead.Add(this);
                                

                                Console.WriteLine("Вы умерли...");
                            }
                            else
                            {
                                this.pobeda += enIndex.Count;
                            }
                            foreach (int k in frIndex)
                            {
                                if (characters[k].lifes <= 0)
                                {
                                    characters[k].lifes = 0;
                                    dead.Add(characters[k]);
                               
                                    Console.WriteLine(" Друг под номером " + (k + 1) + " мертв. ");

                                }
                                else
                                {
                                    characters[k].pobeda += enIndex.Count;
                                }
                            }

                            Console.WriteLine("Вы одержали победу в этой битве! У вас осталось " + this.lifes + " жизней");
                        }

                        else if ((obshayaSilaFr / enIndex.Count < obshayaSilaEn / (frIndex.Count + 1)) || (countfr < counten))
                        {
                            foreach (int k in frIndex)
                            {
                                if (characters[k].lifes <= 0)
                                {
                                    characters[k].lifes = 0;
                                    dead.Add(characters[k]);
                                

                                    Console.WriteLine(" Друг под номером " + (k + 1) + " мертв. ");

                                }
                            }
                            foreach (int i in enIndex)
                            {
                                if (characters[i].lifes > 0)
                                {
                                    characters[i].pobeda += frIndex.Count+1;

                                }
                                else
                                {
                                    characters[i].lifes = 0;
                                    dead.Add(characters[i]);
                                   

                                    Console.WriteLine("Враг под номером " + (i + 1) + " мертв");

                                }

                            }



                            if (this.lifes <= 0)
                            {
                                this.lifes = 0;
                                dead.Add(this);
                                

                                Console.WriteLine("Вы умерли...");
                            }
                            Console.WriteLine("Битва завершилась. Вы проиграли. У вашего персонажа осталось " + this.lifes + " жизней");
                        }


                        else if (obshayaSilaFr/enIndex.Count == obshayaSilaEn/(frIndex.Count + 1) || counten == countfr)
                        {
                            foreach (int i in enIndex)
                            {
                                if (characters[i].lifes <= 0)
                                {
                                    characters[i].lifes = 0;
                                    dead.Add(characters[i]);
                              

                                    Console.WriteLine("Враг под номером " + (i + 1) + " мертв");
                                }
                            }
                            foreach (int k in frIndex)
                            {
                                if (characters[k].lifes <= 0)
                                {
                                    characters[k].lifes = 0;
                                    dead.Add(characters[k]);
                                 

                                    Console.WriteLine(" Друг под номером " + (k + 1) + " мертв.");

                                }

                            }

                            if (this.lifes <= 0)
                            {
                                this.lifes = 0;
                                dead.Add(this);
                                

                                Console.WriteLine("Вы умерли...");
                            }
                            

                            Console.WriteLine("Битва завершилась. Ничья!");

                        }
                        else if (obshayaSilaFr / enIndex.Count < obshayaSilaEn / (frIndex.Count + 1) && counten > countfr)
                        {
                            if (this.lifes <= 0)
                            {
                                dead.Add(this);
                                this.lifes = 0;

                            }
                            else 
                            {
                                this.pobeda += enIndex.Count;
                            }
                           foreach (int k in frIndex)
                            {
                                if (characters[k].lifes <= 0)
                                {
                                    characters[k].lifes = 0;
                                    dead.Add(characters[k]);
                                 

                                    Console.WriteLine(" Друг под номером " + (k + 1) + " мертв!");

                                }
                                else
                                {
                                    characters[k].pobeda += enIndex.Count;
                                }
                            }

                            foreach (int i in enIndex)
                            {
                                if (characters[i].lifes <= 0)
                                {
                                    dead.Add(characters[i]);
                                    characters[i].lifes = 0;

                               

                                    Console.WriteLine("Враг под номером " + (i + 1) + " мертв");
                                }
                            }
                            Console.WriteLine("Сила ваших врагов больше, но у них также больше потери. Вам повезло в этот раз, вы выиграли!");


                        }

                        else if (obshayaSilaFr / enIndex.Count > obshayaSilaEn / (frIndex.Count + 1) && counten < countfr)
                        {
                            foreach (int k in frIndex)
                            {
                                if (characters[k].lifes <= 0)
                                {
                                    characters[k].lifes = 0;
                                    dead.Add(characters[k]);


                                    Console.WriteLine(" Друг под номером " + (k + 1) + " мертв!");

                                }
                                
                            }

                            foreach (int i in enIndex)
                            {
                                if (characters[i].lifes <= 0)
                                {
                                    dead.Add(characters[i]);
                                    characters[i].lifes = 0;



                                    Console.WriteLine("Враг под номером " + (i + 1) + " мертв");
                                }
                                else
                                {
                                    characters[i].pobeda += frIndex.Count;
                                }
                            }
                            Console.WriteLine("Ваша сила больше вражсекой, но у вас также больше потери. Вы проиграли в битве");


                        }

                        break;


                    case 2:

                        foreach (int i in enIndex)
                        {
                            this.destroy(characters[i]);
                            dead.Add(characters[i]);
                         
                        }
                        this.pobeda += enIndex.Count;
                        break;

                }
            }

            enIndex.Clear();
            frIndex.Clear();
        }

        private void moveX(int dx)
        {
            Console.WriteLine("На сколько хотите переместиться?");
            dx = Convert.ToInt32(Console.ReadLine());


            if (dx != 0)
            {
                this.coordX += dx;
            }
            else
            {
                while (dx == 0)
                {
                    Console.WriteLine("Введите значение отличное от нуля");
                }

            }


        }
        private void moveY(int dy)
        {
            Console.WriteLine("На сколько хотите переместиться?");
            dy = Convert.ToInt32(Console.ReadLine());
            if (dy != 0)
            {
                this.coordY += dy;
            }
            else
            {
                while (dy == 0)
                {
                    Console.WriteLine("Введите значение отличное от нуля");
                }
            }

        }
        private void destroy(GameCharacter character)
        {

            character.lifes = 0;
          //  Console.WriteLine("Враг мертв!");


        }


        private void uron(int power, GameCharacter character)
        {

            character.lifes -= power;


        }

        private void heal(List<GameCharacter> characters, List<GameCharacter> dead)
        {
            int count = 0;
            bool check = false;
            int giveLifes;
            switch (this.lager)
            {
                case true:
                    foreach (GameCharacter character in characters)
                    {
                        if (dead.Contains(character) == false && character.lager == true)
                        {
                            count++;
                        }
                    }
                    break;
                case false:
                    foreach (GameCharacter character in characters)
                    {
                        if (dead.Contains(character) == false && character.lager == false)
                        {
                            count++;
                        }
                    }
                    break;

            }

            if ((this.lager == true && count == 1) || (this.lager == false && count == 1) || dead.Contains(this) == true || this.lifes - this.power <= 0)
            {
                Console.WriteLine("Вы не можете никого вылечить");
                check = true;
            }
            

            if (check == false)
            {
                Console.WriteLine("Какому из друзей хотите отдать часть здоровья? Введите номер игрока");
                int nomer = Convert.ToInt32(Console.ReadLine());
                int index;
                foreach (GameCharacter character in characters)
                {
                    if (nomer == character.nomer)
                    {

                        
                        if ( this == character || dead.Contains(character) == true || (character.lifes == 100 || this.lagerProv(character, this) == false) || characters.Contains(character) == false)
                        {
                            
                            Console.WriteLine("Вы не можете лечить этого игрока");
                            
                            break;
                        }
                        
                        else
                        {

                            Console.WriteLine("Сколько жизней хотите отдать? У вас " + this.lifes + " жизней");
                            giveLifes = Convert.ToInt32(Console.ReadLine());
                            if (giveLifes >= this.lifes - this.power || giveLifes > this.power || giveLifes >= this.lifes || giveLifes <= 0 || giveLifes + character.lifes > 100)
                            {
                                while ((giveLifes >= this.lifes - this.power && this.lifes - this.power > 0) || giveLifes > this.power || (giveLifes >= this.lifes && giveLifes <= 0))
                                {
                                    Console.WriteLine("Ошибка ввода. Введите заново:");
                                    giveLifes = Convert.ToInt32(Console.ReadLine());
                                }


                                if (giveLifes + character.lifes > 100)
                                {

                                    Console.WriteLine("Вы ввели количество жизней большее, чем ему требуется до максимального значения, поэтому мы заберем лишь часть этих жизней.");
                                    giveLifes = 100 - character.lifes;

                                }
                            }

                            if (check == false)
                            {

                                character.lifes += giveLifes;
                                this.lifes -= giveLifes;
                                Console.WriteLine("У вашего друга теперь " + character.lifes + " жизней. А у вас " + this.lifes);
                            }


                        }
                    }
                }
            }



        }

        private void vosst(List<GameCharacter> dead)
        {
            if (dead.Contains(this) == false && this.lifes < 100)
            {
                this.lifes = 100;

                Console.WriteLine("У вас теперь максимальное количество жизней 100!");
            }
            else if (dead.Contains(this) == true)
            {
                Console.WriteLine("Вы мертвы, воскрешаться запрещено!");

            }
            else if (dead.Contains(this) == false && this.lifes == 100)
            {
                Console.WriteLine("У вас и так максимальное количество жизней");
            }


        }

        private bool lagerProv(GameCharacter character1, GameCharacter character2)
        {

            if ((character1.lager == true && character2.lager == false) || (character1.lager == false && character2.lager == true))
            {
                return false;
            }
            else
            {
                return true;
            }


        }

       

        public int winProv(List<GameCharacter> characters, List<GameCharacter> dead)
        {
            int countAlFr = 0;
            int countAlEn = 0;
            int prov = 0;
            
            foreach (GameCharacter character in characters)
            {
                if (character.lager == true && dead.Contains(character) == false )//подсчет добрых
                   
                {
                    countAlFr += 1;
                }
               
                else if (character.lager == false && dead.Contains(character) == false)//подсчет злых
                   
                {
                    countAlEn += 1;
                }
                
            }
            if (this.lager == true)
            {
                if (countAlFr > 0 && countAlEn == 0)//вы выиграли
                {
                    prov = 1;
                }
                else if (countAlFr == 0 && countAlEn > 0) //ваша команда проиграла
                {
                
                    prov = 2;
                }
                else if (countAlEn == 0 && countAlFr == 0)
                {
                 
                    prov = 3;
                }
            }
            else if (this.lager == false)
            {
                if (countAlFr > 0 && countAlEn == 0)//вы проиграли
                {
                    prov = 2;
                }
                else if (countAlFr == 0 && countAlEn > 0) //вы выиграли
                {

                    prov = 1;
                }
                else if (countAlEn == 0 && countAlFr == 0)
                {
                    prov = 3;
                }
                
            }
            
            return prov;
        }


    }
}






