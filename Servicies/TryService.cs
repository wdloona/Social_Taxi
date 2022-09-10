using CSharp_React.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_React.Servicies
{
    public class TryService
    {
        const int BlockPeriodSeconds = 30; //Период на который заблокируется пользователь
        const int BlockCheckCounts = 5; //Кол-во попыток доступное за период до блокировки
        const int BlockCheckPeriodSeconds = 20; //Период за который проверяется кол-во попыток

        private static readonly List<TryModel> LoginTries = new();
        private static readonly Dictionary<string, DateTime> LoginBlocks = new();

        public DateTime? CheckLoginTry(TryModel tryModel)
        {
            if (LoginBlocks.ContainsKey(tryModel.IP))
            {
                if (LoginBlocks[tryModel.IP] < tryModel.TryDateTime) //Если время запроса больше, чем время блокировки, то удалим пользователя из словаря блокировок
                    LoginBlocks.Remove(tryModel.IP);
                else
                    return LoginBlocks[tryModel.IP]; //Иначе пользователь ещё заблокирован и вернём ему время блокировки
            }

            var minDate = tryModel.TryDateTime.AddSeconds(-1 * BlockCheckPeriodSeconds);

            LoginTries.RemoveAll(l => l.TryDateTime < minDate); //Удаляем все попыки меньше рассматриваемого промежутка времени

            if (NeedToBlock(LoginTries, tryModel, minDate))
            {
                var blockDateTime = tryModel.TryDateTime.AddSeconds(BlockPeriodSeconds); //Время до которое будет заблокирован пользователь

                if (LoginBlocks.ContainsKey(tryModel.IP))  
                    LoginBlocks[tryModel.IP] = blockDateTime; //Если ВДРУГ пользователь уже заблокирован, то обновим время блокировки
                else
                    LoginBlocks.Add(tryModel.IP, blockDateTime); //Добавим пользователя с словарь блокировок

                return blockDateTime;
            }
            else
                return null;
        }

        private bool NeedToBlock(List<TryModel> list, TryModel tryModel, DateTime minDate)
        {
            var needToBlock = list.Where(l => l.IP == tryModel.IP &&
                                l.TryDateTime <= tryModel.TryDateTime &&
                                l.TryDateTime >= minDate).Count() >= BlockCheckCounts; //Проверим нужно ли заблокировать пользователя
            list.Add(tryModel); //Запишем попытку в лист
            return needToBlock;
        }
    }
}
