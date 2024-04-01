namespace KAMTestStand;

public enum ModemStateE
{
    GsmModemStatusPwrOff, //Питание отключено
    GsmModemStatusHwInit, //Инициализация
    GsmModemStatusGsmRegProcessing, //Регистрация в GSM
    GsmModemStatusGsmRegOk, //Зарегистрирован в GSM
    GsmModemStatus2GRegProcessing, //Регистрация в 2G
    GsmModemStatus2GRegOk, //Зарегистрирован в 2G
    GsmModemStatus3GRegProcessing, //Регистрация в 3G
    GsmModemStatus3GRegOk, //Зарегистрирован в 3G
    GsmModemStatusCallCtrl, //Контроль вызовов
}