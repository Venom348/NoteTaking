namespace NoteTaking.Core.Exceptions;

/// <summary>
///     Класс для вывода сообщения об ошибке у папки
/// </summary>
/// <param name="msg">Сообщение ошибки</param>
public class FolderException(string msg = "") : Exception(msg);