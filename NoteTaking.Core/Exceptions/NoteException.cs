namespace NoteTaking.Core.Exceptions;

/// <summary>
///     Класс для вывода сообщения об ошибке у заметки
/// </summary>
/// <param name="msg">Сообщение ошибки</param>
public class NoteException(string msg = "") : Exception(msg);