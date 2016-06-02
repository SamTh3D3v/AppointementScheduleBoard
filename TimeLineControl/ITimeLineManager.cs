using System;
using DataLayer.Model;

namespace TimeLineControl
{
	public interface ITimeLineManager
	{
		Boolean CanAddToTimeLine(ITimeLineJobTask item);
	}
}
