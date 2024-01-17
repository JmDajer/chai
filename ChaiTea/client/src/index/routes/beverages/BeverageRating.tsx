import { IReview } from '../../domain/IReview';

interface StarRatingProps {
    reviews: IReview[] | null | undefined;
}

function BeverageRating({ reviews }: StarRatingProps) {

    let rating = "0";
    let filledStars = 0;
    let remainingStar = 5;

    console.log(reviews)

    if (reviews != null) {
        
        if (reviews.length > 0) {

        let reviewCount = reviews.length;
        let avgRating = 0;

        reviews.map(review => { avgRating += review.rating })

        rating = Math.round((avgRating * 10 / reviewCount) / 10).toFixed(1);
        filledStars = Math.round(avgRating / reviewCount);
        remainingStar = 5 - filledStars;

        }
    }

    const filledStarElements = Array.from({ length: filledStars }, (_, index) => (
        <span key={index} className="filledStar">&#9733;</span>
    ));

    const remainingStarElements = Array.from({ length: remainingStar }, (_, index) => (
        <span key={index + filledStars} className="emptyStar">&#9733;</span>
    ));

    return (
        <div className="containerRating">
            <span className="rating">{rating}</span>
            <span className="star">
                {filledStarElements}
                {remainingStarElements}
            </span>
        </div>
    );
}

export default BeverageRating;