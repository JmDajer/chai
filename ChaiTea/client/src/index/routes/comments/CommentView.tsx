import React, { useContext, useEffect, useState } from 'react';
import { JwtContext } from '../Root';
import jwtDecode from 'jwt-decode';
import { CommentService } from '../../services/CommentService';
import { IComment } from '../../domain/IComment';

interface ReviewData {
    reviewId: string;
}


const CommentView = (reviewData: ReviewData) => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const commentService = new CommentService(setJwtResponse!, reviewData.reviewId);

    const [validationErrors, setValidationErrors] = useState([] as string[]);
    const [data, setData] = useState([] as IComment[]);

    const [newCommentText, setNewComment] = useState('');

    const [editCommentId, setEditCommentId] = useState('');
    const [editCommentText, setEditCommentText] = useState('');
    const [isEditing, setIsEditing] = useState(false);

    let userName = "";
    let userId = "";
    const maxReviewLength = 512;

    useEffect(() => {
        commentService.getReviewComments(reviewData.reviewId)
            .then(response => {
                if (response) setData(response);
                console.log(data);
            }
            );
    }, []);

    if (jwtResponse != null) {
        let decodedJwt: any = jwtDecode(jwtResponse.jwt);

        userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

        let firstName = decodedJwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'];
        let lastName = decodedJwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname'];
        userName = firstName + " " + lastName;
    }

    const handleEditReview = (comment: IComment) => {
        if (jwtResponse && comment.appUserId === userId) {
            setEditCommentId(comment.id!)
            setEditCommentText(comment.text);
            setIsEditing(true);
        }
    };

    const handleDeleteReview = async (comment: IComment) => {
        if (jwtResponse && comment.appUserId == userId) {
            let deleteResult = await commentService.deleteComment(jwtResponse!, comment.id!, reviewData.reviewId)

            if (deleteResult) {
                window.location.reload();
                console.log('Delete review:', comment);
            }
        }
    };

    const handleReviewSubmit = async () => {
        if (isEditing) {

            const editResult = await commentService.updateComment(jwtResponse!, {
                id: editCommentId,
                reviewId: reviewData.reviewId,
                appUserId: userId,
                name: userName,
                text: editCommentText,
            });

            if (editResult) {
                setEditCommentId('');
                setEditCommentText('');
                console.log('Edit review:', editResult);
            }
        } else {
            let postResult = await commentService.postComment(jwtResponse!, {
                reviewId: reviewData.reviewId,
                appUserId: userId,
                name: userName,
                text: newCommentText,
            });

            if (postResult) {

                // Reset the input fields after submitting the review
                setNewComment('');
            }
        }

    };

    return (
        <>
            <div className="reviewCommentView">
                <div className="commentContainer">
                    <input
                        className="commentTextField"
                        type="text"
                        value={isEditing ? editCommentText : newCommentText}
                        onChange={(e) => {
                            const commentTextValue = e.target.value;

                            if (commentTextValue.length <= maxReviewLength) {
                                if (isEditing) {
                                    setEditCommentText(commentTextValue);
                                } else {
                                    setNewComment(commentTextValue);
                                }
                            } else {
                                setValidationErrors(["Comment is too long!"]);
                            }
                        }}
                        placeholder="Comment"
                    />
                    <div className="commentActions">
                        <button className="submitButton" onClick={handleReviewSubmit}>Submit</button>
                    </div>
                </div>
                {data == null
                    ? ''
                    : data.map((comment) => (
                        <div className="commentContainer" key={comment.id}>
                            <div>
                                <span className="commentContainerText">{comment.name} </span>
                            </div>
                            {comment.text}
                            {comment.appUserId === userId && ( // Check if the review's user ID matches the current user's ID
                                <div className="commentActions">
                                    <button className="editButton" onClick={() => handleEditReview(comment)}>Edit</button>
                                    <button className="deleteButton" onClick={() => handleDeleteReview(comment)}>Delete</button>
                                </div>
                            )}
                        </div>
                    ))}
            </div>
        </>
    );
};

export default CommentView;