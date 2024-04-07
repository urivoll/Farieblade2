using System.Collections;
using TMPro;
using UnityEngine;

public class Coach : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coachText;
    
    [SerializeField] private GameObject black;
    [SerializeField] private GameObject elf;
    [SerializeField] private Animator elfAnimator;
    [SerializeField] private GameObject tr3;
    [SerializeField] private GameObject tr4;

    [SerializeField] private GameObject tr1C;
    [SerializeField] private GameObject tr3C;
    [SerializeField] private GameObject tr5C;
    [SerializeField] private GameObject tr8C;
    [SerializeField] private GameObject tr10C;
    [SerializeField] private GameObject tr11C;
    [SerializeField] private GameObject tr13C;
    [SerializeField] private GameObject tr14C;
    [SerializeField] private GameObject help;
    [SerializeField] private GameObject CollectionReturn;
    [SerializeField] private GameObject FightReturn;
    [SerializeField] private GameObject ToFight;
    [SerializeField] private GameObject pack;

    private bool con = false;
    public void Continue()
    {
        
        con = true;
    }
    private IEnumerator CoachStart()
    {
        if (PlayerData.traning == 1)
        {
            black.SetActive(true);
            tr1C.SetActive(true);
            CollectionReturn.SetActive(false);
            ToFight.SetActive(false);
            FightReturn.SetActive(false);
            if (PlayerData.language == 0) coachText.text = "Welcome to Fairyblade! To start your first battle, click on the first location button";
            else if (PlayerData.language == 1) coachText.text = "Добро пожаловать в Fairyblade! Чтобы начать свой первый бой, нажми на кнопку первой локации";

            while (con == false) yield return null;
            con = false;
        }
        else if (PlayerData.traning == 2)
        {
            elfAnimator.SetTrigger("on");
            black.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "The properties of the upcoming battle are shown here, the enemy squad is shown on the right, yours on the left.";
            else if (PlayerData.language == 1) coachText.text = "Здесь показаны свойства предстоящего боя, справа показан отряд противника, слева твой.";

            while (con == false) yield return null;
            con = false;
            yield return new WaitForSeconds (0.3f);
            PlayerData.traning = 3;
            StartCoroutine(CoachStart());
        }
        else if (PlayerData.traning == 3)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr3.transform.position;
            tr3C.SetActive(true);
            black.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Let's have a look at your card collection, click this button";
            else if (PlayerData.language == 1) coachText.text = "Давай посмотрим на твою стартовую коллекцию карт";

            while (con == false) yield return null;
            con = false;
        }
        else if (PlayerData.traning == 4)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Wow! You have 3 bronze cards in your collection!";
            else if (PlayerData.language == 1) coachText.text = "Ух ты! В твоей коллекции 3 бронзовые карты!";

            while (con == false) yield return null;
            con = false;
            yield return new WaitForSeconds(0.3f);
            PlayerData.traning = 5;
            StartCoroutine(CoachStart());
        }
        else if (PlayerData.traning == 5)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            tr5C.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "So, each card has its own type (Melee, Shooter or Support)";
            else if (PlayerData.language == 1) coachText.text = "Итак, каждая карта имеет свой тип (Ближний бой, Стрелок или Поддержка).";

            while (con == false) yield return null;
            con = false;
            yield return new WaitForSeconds(0.3f);
            PlayerData.traning = 6;
            StartCoroutine(CoachStart());
        }
        else if (PlayerData.traning == 6)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Let's form squad, to do this, drag the 3 available characters to the left panel.";
            else if (PlayerData.language == 1) coachText.text = "Давай сформируем отряд, для этого перетяни 3 доступных персонажа на левую панель.";

            while (con == false) yield return null;
            con = false;
            yield return new WaitForSeconds(0.3f);
            PlayerData.traning = 7;
            StartCoroutine(CoachStart());
        }
        else if (PlayerData.traning == 7)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Remember, shooters and support can only be placed on the second line and melee on the first";
            else if (PlayerData.language == 1) coachText.text = "Помни, стрелков и поддержку можно размещать только на второй линии, а карты ближнего боя — на первой.";

            while (con == false) yield return null;
            con = false;
            while (PlayerData.troopAmount != 3) yield return null;
            PlayerData.traning = 8;
            StartCoroutine(CoachStart());
        }
        else if (PlayerData.traning == 8)
        {
            CollectionReturn.SetActive(true);
            ToFight.SetActive(true);

            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            tr8C.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Okay, the squad is formed. Now you can fight.";
            else if (PlayerData.language == 1) coachText.text = "Хорошо, твой отряд сформирован. Теперь можно сражаться.";
            
            while (con == false) yield return null;
            con = false;
        }
        else if (PlayerData.traning == 9)
        {
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Great battle! Your squad has an energy level that will decrease over time, if the energy drops to zero, the map will be impossible.";
            else if (PlayerData.language == 1) coachText.text = "Отличная битва! У твоего отряда есть уровень энергии, который со временем будет понижаться, если энергия упадет до нуля, карту сыграть будет невозможно.";

            while (con == false) yield return null;
            con = false;
            yield return new WaitForSeconds(0.3f);
            PlayerData.traning = 10;
            StartCoroutine(CoachStart());
        }
        else if (PlayerData.traning == 10)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            tr10C.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "By the way, I have a gift for you, after passing the second location, I will give it to you.";
            else if (PlayerData.language == 1) coachText.text = "Кстати, у меня для тебя подарок, ты получишь его после прохождения второй локации";

            while (con == false) yield return null;
            con = false;
        }
        else if (PlayerData.traning == 11)
        {
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            tr11C.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Great, let's get out of the adventure map.";
            else if (PlayerData.language == 1) coachText.text = "Отлично, давай выйдем из карты приключений.";

            while (con == false) yield return null;
            con = false;
        }
        else if (PlayerData.traning == 14)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            tr14C.SetActive(true);
            pack.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Here, cards of various ranks are usually knocked out. This way...";
            else if (PlayerData.language == 1) coachText.text = "Здесь обычно выбиваются карты различного ранга. Нам сюда...";

            while (con == false) yield return null;
            PlayerData.traning = 15;
            con = false;
        }
        else if (PlayerData.traning == 17)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "Wow, nice card! Don't forget to add this card to your squad.";
            else if (PlayerData.language == 1) coachText.text = "Ого, неплохая карта! Не забудь добавить эту карту в свой отряд.";

            while (con == false) yield return null;
            con = false;
            yield return new WaitForSeconds(0.3f);
            PlayerData.traning = 18;
            StartCoroutine(CoachStart());
        }

        else if (PlayerData.traning == 18)
        {
            elfAnimator.SetTrigger("on");
            elf.transform.position = tr4.transform.position;
            black.SetActive(true);
            tr13C.SetActive(true);
            help.SetActive(true);
            if (PlayerData.language == 0) coachText.text = "That's all for now. For more information click on the icon that looks like this. Good game!";
            else if (PlayerData.language == 1) coachText.text = "Это все на данный момент. Для получения дополнительной информации нажми на значок, который выглядит следующим образом. Хорошей игры!";

            while (con == false) yield return null;
            con = false;
        }
    }
    public void CoachStartGet() => StartCoroutine(CoachStart());
}
