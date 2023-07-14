namespace VideoProcess.NET.Modify;

public class Trim
{
    public int? StartSec { get; set; } 
    public int? EndSec { get; set; } 

    public Trim(int? startSec = null, int? endSec = null)
    {
        if((startSec != null && endSec != null) && startSec > endSec)
        {
            // @TODO FIXME обработку ошибок добавить
            throw new ArgumentException();
        }
        StartSec = startSec;
        EndSec = endSec;
    } 
}