type Props = {
  date: Date;
};

function DateTimeFormat({ date }: Props) {
  const formattedDate = new Intl.DateTimeFormat("sv-SE", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "numeric",
    minute: "numeric",
  }).format(date);

  return <span>{formattedDate}</span>;
}

export default DateTimeFormat;
