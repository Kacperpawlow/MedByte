class TomografList extends React.Component {
    render() {
        return (
            <div className="container">
                <div className="row">
                    {this.props.tomografy.map(tomograf => (
                        <div className="col-md-6 mb-4" key={tomograf.id}>
                            <div className="card h-100">
                                <div className="card-body text-center">
                                    <h5 className="card-title">{tomograf.nazwa}</h5>
                                    <p className="card-text">{tomograf.miasto}</p>
                                    {tomograf.imagePath && (
                                        <img src={tomograf.imagePath} alt="Image" className="img-fluid mb-3" style={{ maxHeight: '300px', maxWidth: '100%' }} />
                                    )}
                                    <div className="d-flex justify-content-between">
                                        <a className="btn btn-secondary btn-lg" href={`/Tomografy/Edit/${tomograf.id}`}>Edit</a>
                                        <a className="btn btn-info btn-lg" href={`/Tomografy/Details/${tomograf.id}`}>Details</a>
                                        <a className="btn btn-danger btn-lg" href={`/Tomografy/Delete/${tomograf.id}`}>Delete</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        );
    }
}

// Fetch Tomografy data and render the React component
document.addEventListener('DOMContentLoaded', function () {
    const container = document.getElementById('tomografy-container');
    if (container) {
        fetch('/Tomografy/GetTomografy')
            .then(response => response.json())
            .then(data => {
                ReactDOM.render(<TomografList tomografy={data} />, container);
            });
    }
});
