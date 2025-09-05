namespace NoteTaking.Core.Exceptions;

/// <summary>
///     Класс для вывода сообщения об ошибке у тега
/// </summary>
/// <param name="msg">Сообщение ошибки</param>
public class TagException(string msg = "") : Exception(msg);