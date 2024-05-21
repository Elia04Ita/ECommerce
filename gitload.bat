current_datetime=$(date '+%d-%m-%Y %H:%M')

git add .

git commit -am "Versione del $current_datetime"

git push

rm "ECommerce.sln"