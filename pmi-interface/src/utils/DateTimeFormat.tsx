function DateTimeFormat(date: string | Date) {
  const d = new Date(date);

  const formattedDate = new Intl.DateTimeFormat("sv-SE", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  }).format(d);

  return formattedDate;
}

export default DateTimeFormat;
